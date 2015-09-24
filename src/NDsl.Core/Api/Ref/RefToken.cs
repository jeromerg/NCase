using JetBrains.Annotations;
using NDsl.Api.Core;

namespace NDsl.Api.Ref
{
    public class RefToken<T> : OwnedToken<T>
    {
        public RefToken([NotNull] T owner, ICodeLocation codeLocation)
            : base(owner, codeLocation)
        {
        }
    }
}