using JetBrains.Annotations;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Util;

namespace NDsl.Back.Api.Def
{
    public class EndToken<T> : OwnedToken<T>
    {
        public EndToken([NotNull] T owner, [NotNull] CodeLocation codeLocation)
            : base(owner, codeLocation)
        {
        }
    }
}