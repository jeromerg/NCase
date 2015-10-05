using NDsl.Back.Api.Core;

namespace NDsl.Back.Api.Ref
{
    public interface IRefNode<out T> : INode
        where T : IDefNode
    {
        T Reference { get; }
    }
}