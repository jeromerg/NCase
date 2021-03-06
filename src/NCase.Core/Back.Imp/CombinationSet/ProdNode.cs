using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.CombinationSet;
using NDsl.All.Def;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Def;
using NDsl.Back.Api.Util;

namespace NCaseFramework.Back.Imp.CombinationSet
{
    public abstract class ProdNode : IProdNode
    {
        [NotNull] private readonly CodeLocation mCodeLocation;
        [NotNull] private readonly ProdId mId;
        [NotNull] private readonly List<IUnionNode> mUnions = new List<IUnionNode>();

        protected ProdNode([NotNull] ProdId id, [NotNull] CodeLocation codeLocation)
        {
            if (codeLocation == null) throw new ArgumentNullException("codeLocation");

            mId = id;
            mCodeLocation = codeLocation;
        }

        IDefId IDefNode.Id
        {
            get { return Id; }
        }

        [NotNull] public ProdId Id
        {
            get { return mId; }
        }

        public IEnumerable<INode> Children
        {
            get { return mUnions; }
        }

        [NotNull, ItemNotNull] public IEnumerable<IUnionNode> Unions
        {
            get { return mUnions; }
        }

        public void AddUnion([NotNull] IUnionNode union)
        {
            mUnions.Add(union);
        }

        [NotNull] public CodeLocation CodeLocation
        {
            get { return mCodeLocation; }
        }
    }
}