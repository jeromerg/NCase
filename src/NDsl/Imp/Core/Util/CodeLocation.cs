using System;
using System.Diagnostics;
using NDsl.Api.Core.Util;
using NVisitor.Common.Quality;

namespace NDsl.Imp.Core.Util
{
    public class CodeLocation : ICodeLocation
    {
        [CanBeNull]
        private readonly StackFrame mStackFrame;

        public CodeLocation([CanBeNull] StackFrame stackFrame)
        {
            if (stackFrame == null) throw new ArgumentNullException("stackFrame");
            mStackFrame = stackFrame;
        }

        public string GetUserCodeInfo()
        {
            if (mStackFrame == null)
                return "no information";

            // TODO Format string a.o. so that Resharper StackTrace functionality works!
            return mStackFrame.ToString();
            
        }
    }
}