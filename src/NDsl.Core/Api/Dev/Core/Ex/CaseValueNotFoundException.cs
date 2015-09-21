using System;

namespace NDsl.Api.Dev.Core.Ex
{
    public class CaseValueNotFoundException : Exception
    {
        public CaseValueNotFoundException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }
    }
}