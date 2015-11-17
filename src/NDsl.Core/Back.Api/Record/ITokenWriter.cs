using System;
using JetBrains.Annotations;
using NDsl.Back.Api.Common;

namespace NDsl.Back.Api.Record
{
    public interface IRecorder
    {
        RecorderMode Mode { get; }
    }
    public interface ITokenWriter : IRecorder
    {
        IDisposable SetWriteMode();
        void SetWriteMode(bool isWriteMode);
        void Append([NotNull] IToken token);
    }
}