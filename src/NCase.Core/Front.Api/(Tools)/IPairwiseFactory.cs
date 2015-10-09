using JetBrains.Annotations;
using NCase.Front.Api;
using NCase.Front.Ui;
using NDsl.All;
using NDsl.Back.Api.Core;

namespace NCase.Front.Imp
{
    public interface IPairwiseFactory : ITool<IBuilderModel>
    {
        IPairwise Create([NotNull] string defName, [NotNull] IBook book);
    }
}