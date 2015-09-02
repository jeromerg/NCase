using NDsl.Api.Core.Util;
using NVisitor.Common.Quality;

namespace NDsl.Api.Core.Tok
{
    public class EndToken<T> : OwnedToken<T>
    {
        public EndToken([NotNull] T semanticalOwner, [NotNull] ICodeLocation codeLocation) : base(semanticalOwner, codeLocation)
        {
        }
    }
}