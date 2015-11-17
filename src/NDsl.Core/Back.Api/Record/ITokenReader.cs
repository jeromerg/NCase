using System.Collections.Generic;
using NDsl.Back.Api.Common;

namespace NDsl.Back.Api.Record
{
    public interface ITokenReader : IRecorder
    {
        IEnumerable<IToken> Tokens { get; }
    }
}