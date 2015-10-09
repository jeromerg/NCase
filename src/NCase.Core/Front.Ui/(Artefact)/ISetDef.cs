using JetBrains.Annotations;
using NCase.Back.Api.Tree;
using NCase.Front.Api;
using NDsl.Front.Ui;

namespace NCase.Front.Ui
{
    public interface ISetDef<out TModel, out TId> : IDef<TModel, TId>
        where TModel : ISetDefModel<TId>
        where TId : ISetDefId
    {
    }
}