using JetBrains.Annotations;
using NDsl.Back.Api.Record;
using NDsl.Back.Api.RecPlay;

namespace NDsl.Back.Api.Builder
{
    public interface IBuilderModel
    {
        [NotNull] ITokenStream TokenStream { get; }
    }
}