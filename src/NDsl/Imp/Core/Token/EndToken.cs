using System;
using NDsl.Api.Core.Util;
using NVisitor.Common.Quality;

namespace NDsl.Imp.Core.Token
{
    public class EndToken<T> : OwnedToken<T>
    {
        public EndToken([NotNull] T semanticalOwner, [NotNull] ICodeLocation codeLocation) : base(semanticalOwner, codeLocation)
        {
        }
    }
}