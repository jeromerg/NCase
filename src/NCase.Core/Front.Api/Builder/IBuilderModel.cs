using JetBrains.Annotations;
using NDsl.Back.Api.Book;
using NDsl.Front.Api;

namespace NCase.Front.Api.Builder
{
    public interface IBuilderModel : IArtefactModel
    {
        [NotNull] IBook Book { get; }
    }
}