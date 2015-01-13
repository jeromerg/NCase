using Castle.DynamicProxy;
using NCase.Api.Dev;

namespace NCase.Component.InterfaceProxy
{
    public class ProxyInterceptor : IInterceptor
    {
        private readonly INode<ITarget> mRoot;
        private readonly IBuilderStrategy mBuilderStrategy;

        public ProxyInterceptor(INode<ITarget> root, IBuilderStrategy builderStrategy)
        {
            mRoot = root;
            mBuilderStrategy = builderStrategy;
        }

        public void Intercept(IInvocation invocation)
        {
            mBuilderStrategy.PlaceChild(mRoot, new InterfaceProxyPropertyNode(invocation));
        }
    }
}
