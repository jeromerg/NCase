using System;
using NDsl.Api.Dev.Core.Util;
using NVisitor.Common.Quality;

namespace NDsl.Api.Dev.Core.Tok
{
    public abstract class Token : IToken
    {
        [NotNull] private readonly ICodeLocation mCodeLocation;

        protected Token([NotNull] ICodeLocation codeLocation)
        {
            if (codeLocation == null) throw new ArgumentNullException("codeLocation");
            mCodeLocation = codeLocation;
        }

        public ICodeLocation CodeLocation
        {
            get { return mCodeLocation; }
        }
    }
}