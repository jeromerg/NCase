using System;
using NDsl.Back.Api.Core;
using NVisitor.Common.Quality;

namespace NDsl.Back.Api.RecPlay
{
    public class InvocationToken<T> : OwnedToken<T>
    {
        [NotNull] private readonly IInvocationRecord mInvocationRecord;

        public InvocationToken(
            [NotNull] T ownerId,
            [NotNull] IInvocationRecord invocationRecord,
            ICodeLocation codeLocation)
            : base(ownerId, codeLocation)
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