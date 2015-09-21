using System;
using NDsl.Api.Dev.Core.Util;
using NVisitor.Common.Quality;

namespace NDsl.Api.Dev.Core.Ex
{
    public class InvalidSyntaxException : Exception
    {
        private readonly ICodeLocation mCodeLocation;

        [StringFormatMethod("args")]
        public InvalidSyntaxException(ICodeLocation codeLocation, string format, params object[] args)
            : base(string.Format("{0}\n\t{1}", codeLocation.GetUserCodeInfo(), string.Format(format, args)))
        {
            mCodeLocation = codeLocation;
        }

        public ICodeLocation CodeLocation
        {
            get { return mCodeLocation; }
        }
    }
}