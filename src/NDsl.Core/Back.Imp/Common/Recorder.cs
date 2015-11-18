using System;
using NDsl.Back.Api.Ex;
using NDsl.Back.Api.Record;
using NDsl.Back.Api.Util;

namespace NDsl.Back.Imp.Common
{
    public class Recorder : IRecorder
    {
        private int mModeIncrement;

        public RecorderMode Mode { get; private set; }

        public IDisposable SetReadMode()
        {
            return new DisposableWithCallbacks(() => SetReadMode(true), () => SetReadMode(false));
        }

        public IDisposable SetWriteMode()
        {
            return new DisposableWithCallbacks(() => SetWriteMode(true), () => SetWriteMode(false));
        }

        public void SetReadMode(bool isReadMode)
        {
            SetMode(isReadMode, RecorderMode.Read);
        }

        public void SetWriteMode(bool isWriteMode)
        {
            SetMode(isWriteMode, RecorderMode.Write);
        }

        private void SetMode(bool isEnteringMode, RecorderMode wishedMode)
        {
            if (Mode != RecorderMode.None && Mode != wishedMode)
                throw new InvalidRecPlayStateException("Cannot set '{0}' mode as Recorder is currently in '{1}' mode",
                                                       wishedMode,
                                                       Mode);

            mModeIncrement += isEnteringMode ? 1 : -1;

            Mode = mModeIncrement > 0 ? wishedMode : RecorderMode.None;
        }
    }
}