using JetBrains.Annotations;
using NCase.Back.Api.Prod;
using NCase.Front.Api;
using NCase.Front.Ui;
using NDsl.All;
using NDsl.Back.Api.Core;

namespace NCase.Front.Imp
{
    public interface IProdFactory : ITool<IBuilderApi>
    {
        IProd Create([NotNull] string defName, [NotNull] IBook book);
    }
}