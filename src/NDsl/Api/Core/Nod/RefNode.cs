﻿using System;
using System.Collections.Generic;
using System.Linq;
using NDsl.Api.Core.Util;
using NVisitor.Common.Quality;

namespace NDsl.Api.Core.Nod
{
    public class RefNode<T> : IRefNode<T>
        where T : INode
    {
        private readonly T mReference;
        private readonly ICodeLocation mCodeLocation;

        public RefNode([NotNull] T reference, [NotNull] ICodeLocation codeLocation)
        {
            if (reference == null) throw new ArgumentNullException("reference");
            if (codeLocation == null) throw new ArgumentNullException("codeLocation");
            
            mReference = reference;
            mCodeLocation = codeLocation;
        }

        public IEnumerable<INode> Children
        {
            get { return Enumerable.Empty<INode>(); }
        }

        public ICodeLocation CodeLocation
        {
            get { return mCodeLocation; }
        }

        public T Reference
        {
            get { return mReference; }
        }
    }
}