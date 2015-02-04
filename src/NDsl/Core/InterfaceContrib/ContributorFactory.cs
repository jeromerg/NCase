using System;
using System.Linq;
using Castle.DynamicProxy;
using NDsl.Api.Dev;

namespace NDsl.Core.InterfaceContrib
{
    /// <summary>in DI-Container</summary>
    public class ContributorFactory : IContributorFactory
    {
        private readonly ProxyGenerator mProxyGenerator;

        public ContributorFactory(ProxyGenerator proxyGenerator)
        {
            mProxyGenerator = proxyGenerator;
        }

        public bool CanHandle<T>()
        {
            return typeof(T).IsInterface;
        }

        public T Create<T>(RootNode rootNode, object[] arguments)
        {
            var interceptor = new ProxyInterceptor(rootNode);
            Type[] interfaces = typeof(T).GetInterfaces().Where(i => (i.IsPublic || i.IsNestedPublic) && !i.IsImport).ToArray();
            return (T)mProxyGenerator.CreateInterfaceProxyWithoutTarget(typeof(T),
                                                                        interfaces, 
                                                                        ProxyGenerationOptions.Default, 
                                                                        interceptor);
        }

    }
}
