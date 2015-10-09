using JetBrains.Annotations;
using NCase.Front.Ui;
using NDsl.Back.Api.Common;

namespace NCase.Front.Api.Fact
{
    public interface IFactFactory
    {
        IFact Create([NotNull] INode fact);
    }
}