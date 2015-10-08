using JetBrains.Annotations;
using NCase.Back.Api.Tree;
using NCase.Front.Api;
using NDsl.Front.Ui;

namespace NCase.Front.Ui
{
    public interface ISetDef<out TApi> : IDef<TApi>
    {
    }
}