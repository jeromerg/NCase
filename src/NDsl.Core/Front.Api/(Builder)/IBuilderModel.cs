using JetBrains.Annotations;
using NDsl.Back.Api.Book;

namespace NDsl.Front.Api
{
    public interface IBuilderModel : IArtefactModel
    {
        [NotNull] ITokenStream TokenStream { get; }
    }
}