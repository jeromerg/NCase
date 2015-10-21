using System.Diagnostics.CodeAnalysis;
using NCaseFramework.Front.Api.Fact;
using NDsl.Front.Api;

namespace NCaseFramework.Front.Ui
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface IFact : IArtefact<IFactModel>
    {
    }
}