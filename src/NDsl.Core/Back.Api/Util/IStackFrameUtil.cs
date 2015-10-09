using System.Diagnostics;
using JetBrains.Annotations;

namespace NDsl.Back.Api.Util
{
    public interface IStackFrameUtil
    {
        [CanBeNull]
        StackFrame GetUserStackFrame();
    }
}