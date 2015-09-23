using NDsl.Back.Api.Core;
using NVisitor.Common.Quality;

namespace NDsl.Back.Api.Block
{
    public class BeginToken<T> : OwnedToken<T>
    {
        public BeginToken([NotNull] T ownerId, ICodeLocation codeLocation)
            : base(ownerId, codeLocation)
        {
        }
    }
}