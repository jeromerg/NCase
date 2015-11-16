using JetBrains.Annotations;
using NDsl.Back.Api.RecPlay;
using NDsl.Back.Api.TokenStream;

namespace NDsl.Back.Api.Builder
{
    public interface IBuilderModel
    {
        [NotNull] ITokenStream TokenStream { get; }
    }
}