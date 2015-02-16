using System;
using NDsl.Api.Core.Util;
using NVisitor.Common.Quality;

namespace NDsl.Impl.Core.Util
{
    public class CodeLocationUtil : ICodeLocationUtil
    {
        [NotNull] private readonly IStackFrameUtil mStackFrameUtil;

        public CodeLocationUtil(IStackFrameUtil stackFrameUtil)
        {
            if (stackFrameUtil == null) throw new ArgumentNullException("stackFrameUtil");
            mStackFrameUtil = stackFrameUtil;
        }

        public ICodeLocation GetUserCodeLocation()
        {
            return new CodeLocation(mStackFrameUtil.GetUserStackFrame());
        }
    }
}
