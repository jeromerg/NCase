using System;
using System.Diagnostics;
using NDsl.Back.Api.Core;
using NVisitor.Common.Quality;

namespace NDsl.Back.Imp.Core
{
    public class CodeLocation : ICodeLocation
    {
        [CanBeNull] private readonly StackFrame mStackFrame;

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

        public override string ToString()
        {
            return GetUserCodeInfo();
        }
    }
}