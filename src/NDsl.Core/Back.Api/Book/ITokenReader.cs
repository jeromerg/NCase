using System.Collections.Generic;
using NDsl.Back.Api.Common;

namespace NDsl.Back.Api.Book
{
    public interface ITokenReader
    {
        IEnumerable<IToken> Tokens { get; }
    }
}