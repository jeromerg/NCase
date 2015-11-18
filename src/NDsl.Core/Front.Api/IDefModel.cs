using JetBrains.Annotations;
using NDsl.All.Def;
using NDsl.Back.Api.Record;

namespace NDsl.Front.Api
{
    public interface IDefModel<out TId>
        where TId : IDefId
    {
        [NotNull] TId Id { get; }
        [NotNull] ITokenStream TokenStream { get; }
    }
}