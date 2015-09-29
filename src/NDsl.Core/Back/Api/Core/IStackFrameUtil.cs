using System.Diagnostics;
using JetBrains.Annotations;

namespace NDsl.Back.Api.Core
{
    public interface IStackFrameUtil
    {
        [CanBeNull]
        StackFrame GetUserStackFrame();
    }
}