using System;
using JetBrains.Annotations;

namespace NDsl.Back.Api.Core
{
    public class InvalidSyntaxException : Exception
    {
        private readonly ICodeLocation mCodeLocation;

        [StringFormatMethod("args")]
        public InvalidSyntaxException(ICodeLocation codeLocation, string format, params object[] args)
            : base(string.Format("{0}\n\t{1}", codeLocation.GetFullInfo(), string.Format(format, args)))
        {
            mCodeLocation = codeLocation;
        }

        public ICodeLocation CodeLocation
        {
            get { return mCodeLocation; }
        }
    }
}