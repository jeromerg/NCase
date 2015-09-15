using System;
using NDsl.Api.Dev.Core.Tok;
using NDsl.Api.Dev.Core.Util;
using NVisitor.Common.Quality;

namespace NDsl.Api.Dev.Core
{
    public class SemanticalBlockDisposable<T> : IDisposable
    {
        [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;
        [NotNull] private readonly ITokenWriter mTokenWriter;
        [NotNull] private readonly T mSemanticalParent;

        public SemanticalBlockDisposable(
            [NotNull] ICodeLocationUtil codeLocationUtil,
            [NotNull] ITokenWriter tokenWriter, 
            [NotNull] T semanticalParent)
        {
            if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");
            if (tokenWriter == null) throw new ArgumentNullException("tokenWriter");
            if (semanticalParent == null) throw new ArgumentNullException("semanticalParent");

            mCodeLocationUtil = codeLocationUtil;
            mTokenWriter = tokenWriter;
            mSemanticalParent = semanticalParent;

            mTokenWriter.Append(new BeginToken<T>(mSemanticalParent, mCodeLocationUtil.GetCurrentUserCodeLocation()));
        }

        public void Dispose()
        {
            mTokenWriter.Append(new EndToken<T>(mSemanticalParent, mCodeLocationUtil.GetCurrentUserCodeLocation()));
        }
    }
}
