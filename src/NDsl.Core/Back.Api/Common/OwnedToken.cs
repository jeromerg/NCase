using System;
using JetBrains.Annotations;
using NDsl.Back.Api.Util;

namespace NDsl.Back.Api.Common
{
    public abstract class OwnedToken<TOwnerId> : Token
    {
        [NotNull] private readonly TOwnerId mOwner;

        protected OwnedToken([NotNull] TOwnerId owner, [NotNull] CodeLocation codeLocation)
            : base(codeLocation)
        {
            if (owner == null) throw new ArgumentNullException("owner");
            mOwner = owner;
        }

        [NotNull] public TOwnerId Owner
        {
            get { return mOwner; }
        }
    }
}