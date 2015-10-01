using JetBrains.Annotations;
using NDsl.Back.Api;
using NDsl.Back.Api.Core;

namespace NCase.Back.Api.Seq
{
    public interface ISeqNode : IDefNode
    {
        [NotNull] SeqId Id { get; }
        void AddChild(INode child);
    }
}