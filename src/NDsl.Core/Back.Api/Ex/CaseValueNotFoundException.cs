using System;

namespace NDsl.Back.Api.Ex
{
    public class CaseValueNotFoundException : Exception
    {
        public CaseValueNotFoundException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }
    }
}