using System;
using NDsl.Api.Core;
using NDsl.Imp.Core.Token;
using NVisitor.Common.Quality;

namespace NDsl.Imp.Core.Reusable
{
    public class SemanticalBlockDisposable<T> : IDisposable
    {
        [NotNull]
        private readonly ITokenWriter mTokenWriter;
        private readonly T mSemanticalParent;

        public SemanticalBlockDisposable([NotNull] ITokenWriter tokenWriter, [NotNull] T semanticalParent)
        {
            if (tokenWriter == null) throw new ArgumentNullException("tokenWriter");
            if (semanticalParent == null) throw new ArgumentNullException("semanticalParent");

            mTokenWriter = tokenWriter;
            mSemanticalParent = semanticalParent;

            mTokenWriter.Append(new BeginToken<T>(mSemanticalParent));
        }

        public void Dispose()
        {
            mTokenWriter.Append(new EndToken<T>(mSemanticalParent));
        }
    }
}
