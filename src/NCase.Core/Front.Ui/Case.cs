using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NCaseFramework.Front.Api.Case;
using NDsl.Front.Ui;

namespace NCaseFramework.Front.Ui
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface Case : Artefact<ICaseModel>
    {
        [NotNull, ItemNotNull] IEnumerable<Fact> Facts { get; }
    }
}