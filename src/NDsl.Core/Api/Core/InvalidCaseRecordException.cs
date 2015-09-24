using System;

namespace NDsl.Api.Core
{
    public class InvalidCaseRecordException : Exception
    {
        public InvalidCaseRecordException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }
    }
}