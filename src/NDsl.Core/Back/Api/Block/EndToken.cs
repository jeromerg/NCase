using JetBrains.Annotations;
using NDsl.Back.Api.Core;

namespace NDsl.Back.Api.Block
{
    public class EndToken<T> : OwnedToken<T>
    {
        public EndToken([NotNull] T owner, [NotNull] ICodeLocation codeLocation)
            : base(owner, codeLocation)
        {
        }
    }
}