using JetBrains.Annotations;
using NDsl.Back.Api.Record;

namespace NDsl.Back.Api.Builder
{
    public interface IBuilderModel
    {
        [NotNull] ITokenStream TokenStream { get; }
    }
}