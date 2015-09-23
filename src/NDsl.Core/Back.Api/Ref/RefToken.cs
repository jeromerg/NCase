using NDsl.Back.Api.Core;
using NVisitor.Common.Quality;

namespace NDsl.Back.Api.Ref
{
    public class RefToken<T> : OwnedToken<T>
    {
        public RefToken([NotNull] T ownerId, ICodeLocation codeLocation)
            : base(ownerId, codeLocation)
        {
        }
    }
}