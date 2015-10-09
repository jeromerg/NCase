using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCase.Back.Api.Prod;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Def;
using NDsl.Back.Api.Util;

namespace NCase.Back.Imp.Prod
{
    public class ProdNode : IProdNode
    {
        [NotNull] private readonly CodeLocation mCodeLocation;
        [NotNull] private readonly List<INode> mDimensions = new List<INode>();

        [NotNull] private readonly ProdId mId;

        public ProdNode(
            [NotNull] CodeLocation codeLocation,
            [NotNull] ProdId id)
        {
            if (codeLocation == null) throw new ArgumentNullException("codeLocation");

            mCodeLocation = codeLocation;

            mId = id;
        }

        IDefId IDefNode.Id
        {
            get { return mId; }
        }

        public ProdId Id
        {
            get { return mId; }
        }

        public IEnumerable<INode> Children
        {
            get { return mDimensions; }
        }

        public void AddChild(INode child)
        {
            mDimensions.Add(child);
        }

        public CodeLocation CodeLocation
        {
            get { return mCodeLocation; }
        }
    }
}