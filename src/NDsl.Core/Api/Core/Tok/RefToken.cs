using NDsl.Api.Core.Util;
using NVisitor.Common.Quality;

namespace NDsl.Api.Core.Tok
{
    public class RefToken<T> : OwnedToken<T>
    {
        public RefToken([NotNull] T ownerReference, ICodeLocation codeLocation)
            : base(ownerReference, codeLocation)
        {
            
        }
    }
}