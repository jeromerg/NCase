using NDsl.Back.Api.Core;

namespace NDsl.Back.Api.Ref
{
    public interface IRefNode<out T> : INode
        where T : INode
    {
        T Reference { get; }
    }
}