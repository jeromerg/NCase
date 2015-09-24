using NDsl.Api.Core;

namespace NDsl.Api.Ref
{
    public interface IRefNode<out T> : INode
        where T : INode
    {
        T Reference { get; }
    }
}