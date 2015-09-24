using System;
using JetBrains.Annotations;

namespace NDsl.Api.Core
{
    public abstract class OwnedToken<TOwnerId> : Token
    {
        [NotNull] private readonly TOwnerId mOwner;

        protected OwnedToken([NotNull] TOwnerId owner, [NotNull] ICodeLocation codeLocation)
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