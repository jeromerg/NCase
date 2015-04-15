using System;
using System.Runtime.Serialization;
using NVisitor.Common.Quality;

namespace NDsl.Api.Core.Ex
{
    public class InvalidCaseRecordException : Exception
    {
        public InvalidCaseRecordException(string format, params object[] args) : base(string.Format(format, args))
        {
        }

        protected InvalidCaseRecordException([NotNull] SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
