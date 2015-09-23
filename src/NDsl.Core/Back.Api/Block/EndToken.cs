using NDsl.Back.Api.Core;
using NVisitor.Common.Quality;

namespace NDsl.Back.Api.Block
{
    public class EndToken<T> : OwnedToken<T>
    {
        public EndToken([NotNull] T ownerId, [NotNull] ICodeLocation codeLocation)
            : base(ownerId, codeLocation)
        {
        }
    }
}