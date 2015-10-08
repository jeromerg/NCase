using System;

namespace NDsl.Back.Api.Core
{
    public class InvalidCaseRecordException : Exception
    {
        public InvalidCaseRecordException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }
    }
}