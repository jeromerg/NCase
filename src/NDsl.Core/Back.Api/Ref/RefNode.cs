﻿using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Def;
using NDsl.Back.Api.Util;

namespace NDsl.Back.Api.Ref
{
    public class RefNode<T> : IRefNode<T>
        where T : IDefNode
    {
        [NotNull] private readonly T mReference;
        [NotNull] private readonly CodeLocation mCodeLocation;

        public RefNode([NotNull] T reference, [NotNull] CodeLocation codeLocation)
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

        public CodeLocation CodeLocation
        {
            get { return mCodeLocation; }
        }

        public T Reference
        {
            get { return mReference; }
        }
    }
}