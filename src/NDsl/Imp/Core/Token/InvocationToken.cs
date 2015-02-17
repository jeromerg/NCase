﻿using System;
using NVisitor.Common.Quality;

namespace NDsl.Imp.Core.Token
{
    public class InvocationToken<T> : OwnedToken<T>
    {
        [NotNull] private readonly IInvocationRecord mInvocationRecord;

        public InvocationToken(
            [NotNull] T semanticalOwner,
            [NotNull] IInvocationRecord invocationRecord) 
            : base(semanticalOwner)
        {
            if (invocationRecord == null) throw new ArgumentNullException("invocationRecord");

            mInvocationRecord = invocationRecord;
        }

        [NotNull]
        public IInvocationRecord InvocationRecord
        {
            get { return mInvocationRecord; }
        }
    }
}
