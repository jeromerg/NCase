using JetBrains.Annotations;
using NCase.Back.Api.SetDef;
using NCase.Back.Api.Tree;
using NCase.Front.Api;
using NCase.Front.Api.SetDef;
using NDsl.Front.Api;

namespace NCase.Front.Ui
{
    public interface ISetDef<out TModel, out TId> : IDef<TModel, TId>
        where TModel : ISetDefModel<TId>
        where TId : ISetDefId
    {
    }
}