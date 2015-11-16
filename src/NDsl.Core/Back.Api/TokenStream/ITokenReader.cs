using System.Collections.Generic;
using NDsl.Back.Api.Common;
using NDsl.Back.Imp.Common;

namespace NDsl.Back.Api.TokenStream
{
    public interface ITokenReader
    {
        TokenStreamMode Mode { get; }
        void SetReadMode(bool isReadMode);
        IEnumerable<IToken> Tokens { get; }
    }
}