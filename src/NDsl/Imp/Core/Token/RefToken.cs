using NDsl.Api.Core.Util;
using NVisitor.Common.Quality;

namespace NDsl.Imp.Core.Token
{
    public class RefToken<T> : OwnedToken<T>
    {
        public RefToken([NotNull] T ownerReference, ICodeLocation codeLocation)
            : base(ownerReference, codeLocation)
        {
            
        }
    }
}