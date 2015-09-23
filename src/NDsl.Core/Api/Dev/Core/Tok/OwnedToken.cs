using System;
using NDsl.Api.Dev.Core.Util;
using NVisitor.Common.Quality;

namespace NDsl.Api.Dev.Core.Tok
{
    public abstract class OwnedToken<TOwnerId> : Token
    {
        [NotNull] private readonly TOwnerId mOwnerId;

        protected OwnedToken([NotNull] TOwnerId ownerId, [NotNull] ICodeLocation codeLocation)
            : base(codeLocation)
        {
            if (ownerId == null) throw new ArgumentNullException("ownerId");
            mOwnerId = ownerId;
        }

        [NotNull] public TOwnerId OwnerId
        {
            get { return mOwnerId; }
        }
    }
}