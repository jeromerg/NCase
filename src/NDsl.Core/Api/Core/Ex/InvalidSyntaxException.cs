using System;
using System.Runtime.Serialization;
using NDsl.Api.Core.Util;
using NVisitor.Common.Quality;

namespace NDsl.Api.Core.Ex
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

        protected InvalidSyntaxException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public ICodeLocation CodeLocation
        {
            get { return mCodeLocation; }
        }
    }
}