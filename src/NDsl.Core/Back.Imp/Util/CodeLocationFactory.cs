using System.Diagnostics;
using JetBrains.Annotations;
using NDsl.Back.Api.Util;

namespace NDsl.Back.Imp.Util
{
    public class CodeLocationFactory : ICodeLocationFactory
    {
        [NotNull] private readonly IUserStackFrameUtil mUserStackFrameUtil;

        public CodeLocationFactory([NotNull] IUserStackFrameUtil userStackFrameUtil)
        {
            mUserStackFrameUtil = userStackFrameUtil;
        }

        [NotNull]
        public CodeLocation GetCurrentUserCodeLocation()
        {
            StackFrame stackFrame = mUserStackFrameUtil.GetUserStackFrame();
            if (stackFrame == null)
                return CodeLocation.Unknown;
            else
                return new CodeLocation(stackFrame.GetFileName() ?? "unknown file",
                                        stackFrame.GetFileLineNumber(),
                                        stackFrame.GetFileColumnNumber());
        }
    }
}