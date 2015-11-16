using System;
using JetBrains.Annotations;
using NDsl.Back.Api.Common;

namespace NDsl.Back.Api.Record
{
    public interface ITokenWriter
    {
        RecorderMode Mode { get; }
        IDisposable SetWriteMode();
        void SetWriteMode(bool isWriteMode);
        void Append([NotNull] IToken token);
    }
}