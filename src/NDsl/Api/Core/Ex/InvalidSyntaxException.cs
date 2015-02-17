using System;
using System.Runtime.Serialization;
using NVisitor.Common.Quality;

namespace NDsl.Api.Core
{
    public class InvaliSyntaxException : Exception
    {
        [StringFormatMethod("args")]
        public InvaliSyntaxException(string format, params object[] args) 
            : base(string.Format(format, args))
        {
        }

        protected InvaliSyntaxException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}