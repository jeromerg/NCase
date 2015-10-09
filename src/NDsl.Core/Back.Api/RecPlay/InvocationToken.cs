using System;
using JetBrains.Annotations;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Util;

namespace NDsl.Back.Api.RecPlay
{
    public class InvocationToken<T> : OwnedToken<T>
    {
        [NotNull] private readonly IInvocationRecord mInvocationRecord;

        public InvocationToken([NotNull] T owner,
                               [NotNull] IInvocationRecord invocationRecord,
                               CodeLocation codeLocation)
            : base(owner, codeLocation)
        {
            if (invocationRecord == null) throw new ArgumentNullException("invocationRecord");

            mInvocationRecord = invocationRecord;
        }

        [NotNull] public IInvocationRecord InvocationRecord
        {
            get { return mInvocationRecord; }
        }
    }
}