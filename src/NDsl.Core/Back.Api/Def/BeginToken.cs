using JetBrains.Annotations;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Util;

namespace NDsl.Back.Api.Def
{
    public class BeginToken<T> : OwnedToken<T>
    {
        public BeginToken([NotNull] T owner, [NotNull] CodeLocation codeLocation)
            : base(owner, codeLocation)
        {
        }
    }
}