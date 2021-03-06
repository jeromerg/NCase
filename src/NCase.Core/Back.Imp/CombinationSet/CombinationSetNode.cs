using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.CombinationSet;
using NDsl.All.Def;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Def;
using NDsl.Back.Api.Util;

namespace NCaseFramework.Back.Imp.CombinationSet
{
    public class CombinationSetNode : ICombinationSetNode
    {
        [NotNull] private readonly CombinationSetId mId;
        [NotNull] private readonly CodeLocation mCodeLocation;
        private readonly bool mIsOnlyPairwise;

        public CombinationSetNode([NotNull] CombinationSetId id, [NotNull] CodeLocation codeLocation, bool isOnlyPairwise)
        {
            if (id == null) throw new ArgumentNullException("id");
            if (codeLocation == null) throw new ArgumentNullException("codeLocation");

            mId = id;
            mCodeLocation = codeLocation;
            mIsOnlyPairwise = isOnlyPairwise;
        }

        IDefId IDefNode.Id
        {
            get { return Id; }
        }

        [NotNull] public CombinationSetId Id
        {
            get { return mId; }
        }

        public CodeLocation CodeLocation
        {
            get { return mCodeLocation; }
        }

        [NotNull, ItemNotNull] public IEnumerable<INode> Children
        {
            get
            {
                return (Product != null)
                           ? new INode[] {Product}
                           : Enumerable.Empty<INode>();
            }
        }

        [CanBeNull] public IProdNode Product { get; set; }

        public bool IsOnlyPairwise
        {
            get { return mIsOnlyPairwise; }
        }
    }
}