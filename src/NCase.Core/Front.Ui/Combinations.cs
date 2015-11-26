using System.Diagnostics.CodeAnalysis;
using NCaseFramework.Back.Api.Tree;
using NCaseFramework.Front.Api.Tree;
using NDsl.Front.Ui;

namespace NCaseFramework.Front.Ui
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface Combinations : SetDefBase<ITreeModel, TreeId, Definer>
    {
    }
}