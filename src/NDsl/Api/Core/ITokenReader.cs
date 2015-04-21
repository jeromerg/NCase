using System.Collections.Generic;

namespace NDsl.Api.Core
{
    public interface ITokenReader
    {
        IEnumerable<IToken> Tokens { get; }
    }
}