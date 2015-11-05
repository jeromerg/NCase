using JetBrains.Annotations;
using NDsl.All.Def;
using NDsl.Back.Api.Book;

namespace NDsl.Front.Api
{
    public interface IDefModel<out TId> : IArtefactModel
        where TId : IDefId
    {
        [NotNull] TId Id { get; }
        ITokenStream TokenStream { get; }
    }
}