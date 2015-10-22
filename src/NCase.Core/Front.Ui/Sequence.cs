using System.Diagnostics.CodeAnalysis;
using NCaseFramework.Back.Api.Seq;
using NCaseFramework.Front.Api.Seq;

namespace NCaseFramework.Front.Ui
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface Sequence : SetDefBase<ISequenceModel, SeqId>
    {
    }
}