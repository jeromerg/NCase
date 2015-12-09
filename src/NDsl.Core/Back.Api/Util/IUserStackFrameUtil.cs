using System.Diagnostics;
using JetBrains.Annotations;

namespace NDsl.Back.Api.Util
{
    public interface IUserStackFrameUtil
    {
        [CanBeNull]
        StackFrame GetUserStackFrame();
    }
}