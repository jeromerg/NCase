using JetBrains.Annotations;
using NCase.Back.Api.Tree;
using NDsl.All;
using NDsl.Front.Ui;

namespace NCase.Front.Api
{
    public interface ISetDefModel<out TId> : IDefModel<TId>
        where TId : ISetDefId
    {
    }
}