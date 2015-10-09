using System;

namespace NDsl.Back.Api.Ex
{
    public class InvalidCaseRecordException : Exception
    {
        public InvalidCaseRecordException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }
    }
}