using System;
using JetBrains.Annotations;

namespace NDsl.Back.Api.Core
{
    public abstract class Token : IToken
    {
        [NotNull] private readonly CodeLocation mCodeLocation;

        protected Token([NotNull] CodeLocation codeLocation)
        {
            if (codeLocation == null) throw new ArgumentNullException("codeLocation");
            mCodeLocation = codeLocation;
        }

        public CodeLocation CodeLocation
        {
            get { return mCodeLocation; }
        }
    }
}