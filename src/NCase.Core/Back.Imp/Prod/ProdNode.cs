using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.Prod;
using NDsl.All.Def;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Def;
using NDsl.Back.Api.Util;

namespace NCaseFramework.Back.Imp.Prod
{
    public class ProdNode : IProdNode
    {
        [NotNull] private readonly CodeLocation mCodeLocation;
        [NotNull] private readonly List<INode> mDimensions = new List<INode>();

        [NotNull] private readonly AllCombinationsId mId;

        public ProdNode(
            [NotNull] CodeLocation codeLocation,
            [NotNull] AllCombinationsId id)
        {
            if (codeLocation == null) throw new ArgumentNullException("codeLocation");

            mCodeLocation = codeLocation;

            mId = id;
        }

        IDefId IDefNode.Id
        {
            get { return mId; }
        }

        public AllCombinationsId Id
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