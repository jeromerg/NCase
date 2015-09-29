using System;
using JetBrains.Annotations;
using NDsl.Back.Api.Core;

namespace NDsl.Back.Imp.Core
{
    public class CodeLocationUtil : ICodeLocationUtil
    {
        [NotNull] private readonly IStackFrameUtil mStackFrameUtil;

        public CodeLocationUtil(IStackFrameUtil stackFrameUtil)
        {
            if (stackFrameUtil == null) throw new ArgumentNullException("stackFrameUtil");
            mStackFrameUtil = stackFrameUtil;
        }

        public ICodeLocation GetCurrentUserCodeLocation()
        {
            return new CodeLocation(mStackFrameUtil.GetUserStackFrame());
        }
    }
}