using NDsl.Back.Api.Common;
using NDsl.Back.Api.Record;

namespace NCaseFramework.Front.Api.Fact
{
    public interface IFactModel
    {
        // TODO JRG: ADD STREAM TO CHANGE MODE (MAYBE ONLY A (NEW) BASE INTERFACE CONTAINING ONLY MODE
        INode FactNode { get; }
        IRecorder Recorder { get; }
    }
}