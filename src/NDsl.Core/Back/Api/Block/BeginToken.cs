using JetBrains.Annotations;
using NDsl.Back.Api.Core;

namespace NDsl.Back.Api.Block
{
    public class BeginToken<T> : OwnedToken<T>
    {
        public BeginToken([NotNull] T owner, CodeLocation codeLocation)
            : base(owner, codeLocation)
        {
        }
    }
}