using JetBrains.Annotations;
using NDsl.Back.Api.Core;

namespace NDsl.Back.Api.Ref
{
    public class RefToken<T> : OwnedToken<T>
    {
        public RefToken([NotNull] T owner, ICodeLocation codeLocation)
            : base(owner, codeLocation)
        {
        }
    }
}