using NDsl.Api.Dev.Core.Util;
using NVisitor.Common.Quality;

namespace NDsl.Api.Dev.Core.Tok
{
    public class RefToken<T> : OwnedToken<T>
    {
        public RefToken([NotNull] T ownerId, ICodeLocation codeLocation)
            : base(ownerId, codeLocation)
        {
        }
    }
}