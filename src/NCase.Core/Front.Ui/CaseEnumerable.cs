using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NCaseFramework.Front.Api.CaseEnumerable;
using NDsl.Front.Api;
using NDsl.Front.Ui;

namespace NCaseFramework.Front.Ui
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface CaseEnumerable : Artefact<ICaseEnumerableModel>, IEnumerable<Case>
    {
    }
}