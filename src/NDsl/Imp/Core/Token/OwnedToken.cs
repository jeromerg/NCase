using System;
using NDsl.Api.Core.Util;
using NVisitor.Common.Quality;

namespace NDsl.Imp.Core.Token
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
