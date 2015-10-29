using System;

namespace NDsl.Back.Api.Ex
{
    public class InvalidRecPlayStateException : Exception
    {
        public InvalidRecPlayStateException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }

        public InvalidRecPlayStateException(Exception innerException, string format, params object[] args)
            : base(string.Format(format, args), innerException)
        {
        }
    }
}