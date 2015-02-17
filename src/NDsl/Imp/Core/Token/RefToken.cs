using NVisitor.Common.Quality;

namespace NDsl.Imp.Core.Token
{
    public class RefToken<T> : OwnedToken<T>
    {
        public RefToken([NotNull] T ownerReference) : base(ownerReference) { }
    }
}