using System;
using System.Linq;
using Castle.DynamicProxy;
using NCase.Api.Dev;

namespace NCase.Component.InterfaceProxy
{
    /// <summary>in DI-Container</summary>
    public class ComponentFactory : IComponentFactory
    {
        private readonly ProxyGenerator mProxyGenerator;

        public ComponentFactory(ProxyGenerator proxyGenerator)
        {
            mProxyGenerator = proxyGenerator;
        }

        public bool CanHandle<T>()
        {
            return typeof(T).IsInterface;
        }

        public T Create<T>(INode<ITarget> rootNode, IBuilderStrategy builderStrategy, object[] arguments)
        {
            var interceptor = new ProxyInterceptor(rootNode, builderStrategy);
            Type[] interfaces = typeof(T).GetInterfaces().Where(i => (i.IsPublic || i.IsNestedPublic) && !i.IsImport).ToArray();
            return (T)mProxyGenerator.CreateInterfaceProxyWithoutTarget(typeof(T),
                                                                        interfaces, 
                                                                        ProxyGenerationOptions.Default, 
                                                                        interceptor);
        }

    }
}
