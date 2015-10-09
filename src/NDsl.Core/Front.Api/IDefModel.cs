using JetBrains.Annotations;
using NDsl.Back.Api.Book;
using NDsl.Back.Api.Def;

namespace NDsl.Front.Api
{
    public interface IDefModel<out TId> : IArtefactModel
        where TId : IDefId
    {
        [NotNull] TId Id { get; }
        ITokenStream TokenStream { get; }
    }
}