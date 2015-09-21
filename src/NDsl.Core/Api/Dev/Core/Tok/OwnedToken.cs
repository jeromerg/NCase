using System;
using NDsl.Api.Dev.Core.Util;
using NVisitor.Common.Quality;

namespace NDsl.Api.Dev.Core.Tok
{
    public abstract class OwnedToken<TSemanticalOwner> : Token
    {
        [NotNull] private readonly TSemanticalOwner mOwner;

        protected OwnedToken([NotNull] TSemanticalOwner owner, [NotNull] ICodeLocation codeLocation)
            : base(codeLocation)
        {
            if (owner == null) throw new ArgumentNullException("owner");
            mOwner = owner;
        }

        [NotNull] public TSemanticalOwner Owner
        {
            get { return mOwner; }
        }
    }
}