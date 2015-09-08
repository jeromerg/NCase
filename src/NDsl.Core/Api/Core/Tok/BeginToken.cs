﻿using NDsl.Api.Core.Util;
using NVisitor.Common.Quality;

namespace NDsl.Api.Core.Tok
{
    public class BeginToken<T> : OwnedToken<T>
    {
        public BeginToken([NotNull] T semanticalOwner, ICodeLocation codeLocation)
            : base(semanticalOwner, codeLocation)
        {
            
        }
    }
}