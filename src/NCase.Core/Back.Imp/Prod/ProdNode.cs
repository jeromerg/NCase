using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCase.Back.Api.Prod;
using NDsl.Api.Core;


namespace NCase.Back.Imp.Prod
{
    public class ProdNode : IProdNode
    {
        [NotNull] private readonly ICodeLocation mCodeLocation;
        [NotNull] private readonly List<INode> mDimensions = new List<INode>();

        [CanBeNull] private readonly ProdId mId;

        public ProdNode(
            [NotNull] ICodeLocation codeLocation,
            [CanBeNull] ProdId id)
        {
            if (codeLocation == null) throw new ArgumentNullException("codeLocation");

            mCodeLocation = codeLocation;

            mId = id;
        }

        public IEnumerable<INode> Children
        {
            get { return mDimensions; }
        }

        public void AddChild(INode child)
        {
            mDimensions.Add(child);
        }

        public ICodeLocation CodeLocation
        {
            get { return mCodeLocation; }
        }
    }
}