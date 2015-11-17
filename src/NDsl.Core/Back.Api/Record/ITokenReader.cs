using System;
using System.Collections.Generic;
using NDsl.Back.Api.Common;

namespace NDsl.Back.Api.Record
{
    public interface ITokenReader : IRecorder
    {
        IDisposable SetReadMode();
        void SetReadMode(bool isReadMode);
        IEnumerable<IToken> Tokens { get; }
    }
}