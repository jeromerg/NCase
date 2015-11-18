using System;
using JetBrains.Annotations;
using NDsl.Back.Api.Util;

namespace NDsl.Back.Api.Ex
{
    public class InvalidSyntaxException : Exception
    {
        [NotNull] private readonly CodeLocation mCodeLocation;

        [StringFormatMethod("args")]
        public InvalidSyntaxException([NotNull] CodeLocation codeLocation, [NotNull] string format, [NotNull] params object[] args)
            : base(string.Format("{0}\n\t{1}", codeLocation.GetFullInfoWithSameSyntaxAsStackTrace(), string.Format(format, args)))
        {
            mCodeLocation = codeLocation;
        }

        [NotNull] 
        public CodeLocation CodeLocation
        {
            get { return mCodeLocation; }
        }
    }
}