﻿using System;
using NDsl.Api.Core;
using NDsl.Api.Core.Util;
using NVisitor.Common.Quality;

namespace NDsl.Imp.Core.Token
{
    public class Token<TSemanticalOwner> : IToken
    {
        [NotNull] private readonly ICodeLocation mCodeLocation;

        public Token([NotNull] ICodeLocation codeLocation)
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