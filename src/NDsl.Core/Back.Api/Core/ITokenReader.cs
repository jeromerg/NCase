using System.Collections.Generic;

namespace NDsl.Back.Api.Core
{
    public interface ITokenReader
    {
        IEnumerable<IToken> Tokens { get; }
    }
}