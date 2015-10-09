using JetBrains.Annotations;
using NCase.Front.Api.Builder;
using NCase.Front.Ui;
using NDsl.Back.Api.Book;
using NDsl.Back.Api.Util;

namespace NCase.Front.Api.Pairwise
{
    public interface IPairwiseFactory : IService<IBuilderModel>
    {
        IPairwise Create([NotNull] string defName, [NotNull] IBook book);
    }
}