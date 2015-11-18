using JetBrains.Annotations;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Record;

namespace NCaseFramework.Front.Api.Fact
{
    public interface IFactFactory
    {
        [NotNull]
        Ui.Fact Create([NotNull] INode fact, [NotNull] IRecorder recorder);
    }
}