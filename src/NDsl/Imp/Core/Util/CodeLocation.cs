using System;
using System.Diagnostics;
using NDsl.Api.Core.Util;
using NVisitor.Common.Quality;

namespace NDsl.Impl.Core.Util
{
    public class CodeLocation : ICodeLocation
    {
        [NotNull]
        private readonly StackFrame mStackFrame;

        public CodeLocation([NotNull] StackFrame stackFrame)
        {
            if (stackFrame == null) throw new ArgumentNullException("stackFrame");
            mStackFrame = stackFrame;
        }

        public string GetUserCodeInfo()
        {
            // TODO Format string a.o. so that Resharper StackTrace functionality works!
            return mStackFrame.ToString();
        }
    }
}