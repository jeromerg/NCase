using JetBrains.Annotations;
using NCaseFramework.Front.Ui;
using NDsl.Back.Api.Common;

namespace NCaseFramework.Front.Api.Fact
{
    public interface IFactFactory
    {
        Ui.Fact Create([NotNull] INode fact);
    }
}