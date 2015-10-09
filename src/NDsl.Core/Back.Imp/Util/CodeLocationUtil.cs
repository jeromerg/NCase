using System;
using System.Diagnostics;
using JetBrains.Annotations;
using NDsl.Back.Api.Util;

namespace NDsl.Back.Imp.Util
{
    public class CodeLocationUtil : ICodeLocationUtil
    {
        [NotNull] private readonly IStackFrameUtil mStackFrameUtil;

        public CodeLocationUtil(IStackFrameUtil stackFrameUtil)
        {
            if (stackFrameUtil == null) throw new ArgumentNullException("stackFrameUtil");
            mStackFrameUtil = stackFrameUtil;
        }

        public CodeLocation GetCurrentUserCodeLocation()
        {
            StackFrame stackFrame = mStackFrameUtil.GetUserStackFrame();
            if (stackFrame == null)
                return CodeLocation.Unknown;
            else
                return new CodeLocation(stackFrame.GetFileName() ?? "unknown file",
                                        stackFrame.GetFileLineNumber(),
                                        stackFrame.GetFileColumnNumber());
        }
    }
}