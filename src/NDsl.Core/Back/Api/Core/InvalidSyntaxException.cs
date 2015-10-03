using System;
using JetBrains.Annotations;

namespace NDsl.Back.Api.Core
{
    public class InvalidSyntaxException : Exception
    {
        private readonly CodeLocation mCodeLocation;

        [StringFormatMethod("args")]
        public InvalidSyntaxException(CodeLocation codeLocation, string format, params object[] args)
            : base(string.Format("{0}\n\t{1}", codeLocation.GetFullInfo(), string.Format(format, args)))
        {
            mCodeLocation = codeLocation;
        }

        public CodeLocation CodeLocation
        {
            get { return mCodeLocation; }
        }
    }
}