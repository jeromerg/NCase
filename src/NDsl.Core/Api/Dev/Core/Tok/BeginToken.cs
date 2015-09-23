using NDsl.Api.Dev.Core.Util;
using NVisitor.Common.Quality;

namespace NDsl.Api.Dev.Core.Tok
{
    public class BeginToken<T> : OwnedToken<T>
    {
        public BeginToken([NotNull] T ownerId, ICodeLocation codeLocation)
            : base(ownerId, codeLocation)
        {
        }
    }
}