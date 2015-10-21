using JetBrains.Annotations;
using NCaseFramework.Back.Api.SetDef;
using NDsl.Back.Api.Common;

namespace NCaseFramework.Back.Api.Seq
{
    public interface ISeqNode : ISetDefNode
    {
        [NotNull] new SeqId Id { get; }
        void AddChild(INode child);
    }
}