using System;
using System.Diagnostics;
using JetBrains.Annotations;
using NDsl.Back.Api.Core;

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

        public string GetFullInfo()
        {
            if (mStackFrame == null)
                return "file unknown";

            // TODO Format string a.o. so that Resharper StackTrace functionality works!
            return mStackFrame.ToString();
        }

        public string GetLineAndColumnInfo()
        {
            if (mStackFrame == null || Line == null)
                return "(line unknown)";

            return string.Format(" ({0}:{1})", Line, Column ?? -1);
        }

        public string FileName
        {
            get { return mStackFrame == null ? null : mStackFrame.GetFileName(); }
        }

        public int? Line
        {
            get { return mStackFrame == null ? (int?) null : mStackFrame.GetFileLineNumber(); }
        }

        public int? Column
        {
            get { return mStackFrame == null ? (int?) null : mStackFrame.GetFileColumnNumber(); }
        }

        public override string ToString()
        {
            return GetFullInfo();
        }
    }
}