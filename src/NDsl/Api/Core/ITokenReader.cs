using System.Collections.Generic;
using NDsl.Api.Core.Tok;

namespace NDsl.Api.Core
{
    public interface ITokenReader
    {
        IEnumerable<IToken> Tokens { get; }
    }
}