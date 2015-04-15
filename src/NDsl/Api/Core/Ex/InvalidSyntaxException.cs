using System;
using System.Runtime.Serialization;
using NVisitor.Common.Quality;

namespace NDsl.Api.Core.Ex
{
    public class InvalidSyntaxException : Exception
    {
        [StringFormatMethod("args")]
        public InvalidSyntaxException(string format, params object[] args) 
            : base(string.Format(format, args))
        {
        }

        protected InvalidSyntaxException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}