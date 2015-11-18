using System;
using JetBrains.Annotations;

namespace NDsl.Back.Api.Ex
{
    public class InvalidRecPlayStateException : Exception
    {
        public InvalidRecPlayStateException([NotNull] string format, [NotNull] params object[] args)
            : base(string.Format(format, args))
        {
        }

        public InvalidRecPlayStateException([NotNull] Exception innerException, [NotNull] string format, [NotNull] params object[] args)
            : base(string.Format(format, args), innerException)
        {
        }
    }
}