using NVisitor.Common.Quality;

namespace NDsl.Imp.Core.Token
{
    public class BeginToken<T> : OwnedToken<T>
    {
        public BeginToken([NotNull] T semanticalOwner) : base(semanticalOwner) { }
    }
}