using JetBrains.Annotations;
using NCase.Back.Api.Tree;
using NDsl.Back.Api.Core;

namespace NCase.Back.Api.Seq
{
    public interface ISeqNode : ISetDefNode
    {
        [NotNull] new SeqId Id { get; }
        void AddChild(INode child);
    }
}