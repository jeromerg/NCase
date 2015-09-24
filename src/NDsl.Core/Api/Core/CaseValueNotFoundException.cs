using System;

namespace NDsl.Api.Core
{
    public class CaseValueNotFoundException : Exception
    {
        public CaseValueNotFoundException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }
    }
}