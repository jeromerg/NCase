using JetBrains.Annotations;
using NDsl.Back.Api.Common;
using NDsl.Back.Imp.Common;

namespace NDsl.Back.Api.TokenStream
{
    public interface ITokenWriter
    {
        TokenStreamMode Mode { get; }
        void SetWriteMode(bool isWriteMode);
        void Append([NotNull] IToken token);
    }
}