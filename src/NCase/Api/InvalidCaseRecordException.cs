using System;
using System.Runtime.Serialization;
using NCase.Util.Quality;

namespace NCase.Api
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
