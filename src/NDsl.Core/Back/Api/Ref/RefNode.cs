using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NDsl.Back.Api.Core;

namespace NDsl.Back.Api.Ref
{
    public class RefNode<T> : IRefNode<T>
        where T : IDefNode
    {
        private readonly T mReference;
        private readonly CodeLocation mCodeLocation;

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