using System.Diagnostics.CodeAnalysis;
using NCaseFramework.Back.Api.SetDef;
using NCaseFramework.Front.Api.SetDef;
using NDsl.Front.Api;

namespace NCaseFramework.Front.Ui
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface SetDefImp<out TModel, out TId> : IDef<TModel, TId>
        where TModel : ISetDefModel<TId>
        where TId : ISetDefId
    {
    }
}