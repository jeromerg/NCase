using JetBrains.Annotations;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Def;

namespace NDsl.Back.Api.Ref
{
    public interface IRefNode<out T> : INode
        where T : IDefNode
    {
        [NotNull]
        T Reference { get; }
    }
}