using NCase.Back.Api.Seq;
using NCase.Front.Api;

namespace NCase.Front.Ui
{
    public interface ISeq : ISetDef<SeqId, ISeqApi>
    {
    }
}