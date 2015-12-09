using System;
using JetBrains.Annotations;

namespace NDsl.Back.Api.Util
{
    public class CodeLocationPrinter : ICodeLocationPrinter
    {
        public string Print([NotNull] CodeLocation codeLocation)
        {
            if (codeLocation == null) throw new ArgumentNullException("codeLocation");
            
            return string.Format("{0}: line {1}",
                                 codeLocation.FileName ?? "unknown file",
                                 codeLocation.Line.HasValue ? codeLocation.Line.Value.ToString() : "??");
        }
    }
}