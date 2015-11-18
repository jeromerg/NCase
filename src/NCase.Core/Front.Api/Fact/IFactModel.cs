using JetBrains.Annotations;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Record;

namespace NCaseFramework.Front.Api.Fact
{
    public interface IFactModel
    {
        [NotNull] INode FactNode { get; }
        [NotNull] IRecorder Recorder { get; }
    }
}