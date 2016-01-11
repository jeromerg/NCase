using JetBrains.Annotations;
using NDsl.Back.Api.Ex;
using NDsl.Back.Api.Util;

namespace NCaseFramework.Back.Api.Combinations
{
    public class IndentationException : InvalidSyntaxException
    {
        public IndentationException([NotNull] ICodeLocationPrinter codeLocationPrinter, [NotNull] CodeLocation codeLocation, [NotNull] string format, [NotNull] params object[] args)
            : base(codeLocationPrinter, codeLocation, format, args)
        {
        }
    }
}