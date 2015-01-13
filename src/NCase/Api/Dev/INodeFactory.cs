using Castle.DynamicProxy;

namespace NCase.Api.Dev
{
    public interface INodeFactory
    {
        INode<ITarget> Create(IInvocation invocation);
    }
}