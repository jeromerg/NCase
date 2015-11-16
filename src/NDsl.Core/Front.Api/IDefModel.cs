using JetBrains.Annotations;
using NDsl.All.Def;
using NDsl.Back.Api.TokenStream;

namespace NDsl.Front.Api
{
    public interface IDefModel<out TId>
        where TId : IDefId
    {
        [NotNull] TId Id { get; }
        ITokenStream TokenStream { get; }
    }
}