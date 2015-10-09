using JetBrains.Annotations;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Util;

namespace NDsl.Back.Api.Ref
{
    public class RefToken<T> : OwnedToken<T>
    {
        public RefToken([NotNull] T owner, CodeLocation codeLocation)
            : base(owner, codeLocation)
        {
        }
    }
}