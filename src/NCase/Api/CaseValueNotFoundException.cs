using System;
using System.Runtime.Serialization;
using NCase.Util.Quality;

namespace NCase.Api
{
    public class CaseValueNotFoundException : Exception
    {
        public CaseValueNotFoundException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }

        protected CaseValueNotFoundException([NotNull] SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
