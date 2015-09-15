using System.Collections.Generic;
using NDsl.Api.Dev.Core.Tok;

namespace NDsl.Api.Dev.Core
{
    public interface ITokenReader
    {
        IEnumerable<IToken> Tokens { get; }
    }
}