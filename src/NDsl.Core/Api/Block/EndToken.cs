using JetBrains.Annotations;
using NDsl.Api.Core;

namespace NDsl.Api.Block
{
    public class EndToken<T> : OwnedToken<T>
    {
        public EndToken([NotNull] T owner, [NotNull] ICodeLocation codeLocation)
            : base(owner, codeLocation)
        {
        }
    }
}