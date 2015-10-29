using System.Diagnostics.CodeAnalysis;
using NCaseFramework.Back.Api.SetDef;
using NCaseFramework.Front.Api.SetDef;
using NDsl.Front.Api;
using NDsl.Front.Ui;

namespace NCaseFramework.Front.Ui
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface SetDefBase<out TModel, out TId> : DefBase<TModel, TId>
        where TModel : ISetDefModel<TId>
        where TId : ISetDefId
    {
    }
}