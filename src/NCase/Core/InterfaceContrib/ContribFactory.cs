using System;
using System.Linq;
using Castle.DynamicProxy;
using NCase.Api.Dev;

namespace NCase.Core.InterfaceContrib
{
    /// <summary>in DI-Container</summary>
    public class ContribFactory : IContribFactory
    {
        private readonly ProxyGenerator mProxyGenerator;

        public ContribFactory(ProxyGenerator proxyGenerator)
        {
            mProxyGenerator = proxyGenerator;
        }

        public bool CanHandle<T>()
        {
            return typeof(T).IsInterface;
        }

        public T Create<T>(AstNode astNode, object[] arguments)
        {
            var interceptor = new ProxyInterceptor(astNode);
            Type[] interfaces = typeof(T).GetInterfaces().Where(i => (i.IsPublic || i.IsNestedPublic) && !i.IsImport).ToArray();
            return (T)mProxyGenerator.CreateInterfaceProxyWithoutTarget(typeof(T),
                                                                        interfaces, 
                                                                        ProxyGenerationOptions.Default, 
                                                                        interceptor);
        }

    }
}
