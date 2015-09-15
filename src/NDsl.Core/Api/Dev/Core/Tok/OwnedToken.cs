using System;
using NDsl.Api.Dev.Core.Util;
using NVisitor.Common.Quality;

namespace NDsl.Api.Dev.Core.Tok
{
    public class OwnedToken<TSemanticalOwner> : Token<TSemanticalOwner>
    {
        [NotNull]
        private readonly TSemanticalOwner mOwner;

        public OwnedToken([NotNull] TSemanticalOwner owner, [NotNull] ICodeLocation codeLocation) 
            : base(codeLocation)
        {
            if (owner == null) throw new ArgumentNullException("owner");
            mOwner = owner;
        }

        [NotNull]
        public TSemanticalOwner Owner
        {
            get { return mOwner; }
        }
    }
}
