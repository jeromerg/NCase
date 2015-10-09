using JetBrains.Annotations;
using NCase.Back.Api.SetDef;
using NDsl.Back.Api.Common;

namespace NCase.Back.Api.Seq
{
    public interface ISeqNode : ISetDefNode
    {
        [NotNull] new SeqId Id { get; }
        void AddChild(INode child);
    }
}