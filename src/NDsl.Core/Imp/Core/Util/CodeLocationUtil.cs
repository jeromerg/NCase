using System;
using NDsl.Api.Dev.Core.Util;
using NVisitor.Common.Quality;

namespace NDsl.Imp.Core.Util
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