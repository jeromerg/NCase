using System;

namespace NDsl.Back.Api.Record
{
    public interface IRecorder
    {
        RecorderMode Mode { get; }
        IDisposable SetReadMode();
        IDisposable SetWriteMode();
        void SetReadMode(bool isReadMode);
        void SetWriteMode(bool isWriteMode);
    }
}