using System;
using JetBrains.Annotations;
using NDsl.Back.Api.Util;

namespace NDsl.Back.Api.Common
{
    public abstract class Token : IToken
    {
        [NotNull] private readonly CodeLocation mCodeLocation;

        protected Token([NotNull] CodeLocation codeLocation)
        {
            if (codeLocation == null) throw new ArgumentNullException("codeLocation");
            mCodeLocation = codeLocation;
        }

        [NotNull] public CodeLocation CodeLocation
        {
            get { return mCodeLocation; }
        }
    }
}