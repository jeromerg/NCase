using JetBrains.Annotations;
using NDsl.Api.Core;

namespace NDsl.Api.Block
{
    public class BeginToken<T> : OwnedToken<T>
    {
        public BeginToken([NotNull] T owner, ICodeLocation codeLocation)
            : base(owner, codeLocation)
        {
        }
    }
}