using NDsl.Api.Dev.Core.Util;
using NVisitor.Common.Quality;

namespace NDsl.Api.Dev.Core.Tok
{
    public class EndToken<T> : OwnedToken<T>
    {
        public EndToken([NotNull] T semanticalOwner, [NotNull] ICodeLocation codeLocation)
            : base(semanticalOwner, codeLocation)
        {
        }
    }
}