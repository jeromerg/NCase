using System;
using System.Runtime.Serialization;
using NVisitor.Common.Quality;

namespace NDsl.Api.Core
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
