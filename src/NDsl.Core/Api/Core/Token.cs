using System;
using JetBrains.Annotations;

namespace NDsl.Api.Core
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