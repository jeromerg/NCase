using System;
using JetBrains.Annotations;

namespace NDsl.Back.Api.Ex
{
    public class CaseValueNotFoundException : Exception
    {
        public CaseValueNotFoundException([NotNull] string format, [NotNull] params object[] args)
            : base(string.Format(format, args))
        {
        }
    }
}