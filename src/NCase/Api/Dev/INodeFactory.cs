using Castle.DynamicProxy;

namespace NTestCase.Api.Dev
{
    public interface INodeFactory
    {
        INode<ITarget> Create(IInvocation invocation);
    }
}