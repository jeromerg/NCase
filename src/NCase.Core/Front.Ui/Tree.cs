using System.Diagnostics.CodeAnalysis;
using NCaseFramework.Back.Api.Tree;
using NCaseFramework.Front.Api.Tree;

namespace NCaseFramework.Front.Ui
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface Tree : SetDefImp<ITreeModel, TreeId>
    {
    }
}