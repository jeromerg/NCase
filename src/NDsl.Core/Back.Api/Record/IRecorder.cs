using System;
using JetBrains.Annotations;

namespace NDsl.Back.Api.Record
{
    public interface IRecorder
    {
        RecorderMode Mode { get; }
        [NotNull] IDisposable SetReadMode();
        [NotNull] IDisposable SetWriteMode();
        void SetReadMode(bool isReadMode);
        void SetWriteMode(bool isWriteMode);
    }
}