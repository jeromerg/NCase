using System.Diagnostics;
using JetBrains.Annotations;

namespace NDsl.Api.Core
{
    public interface IStackFrameUtil
    {
        [CanBeNull]
        StackFrame GetUserStackFrame();
    }
}