using JetBrains.Annotations;
using NDsl.Back.Api.Ex;
using NDsl.Back.Api.Util;

namespace NCaseFramework.Back.Api.Combinations
{
    public class IndentationException : InvalidSyntaxException
    {
        public IndentationException([NotNull] CodeLocation codeLocation, [NotNull] string format, [NotNull] params object[] args)
            : base(codeLocation, format, args)
        {
        }
    }
}