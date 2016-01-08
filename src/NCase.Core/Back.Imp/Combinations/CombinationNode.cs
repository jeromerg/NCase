using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.Combinations;
using NDsl.All.Def;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Def;
using NDsl.Back.Api.Util;

namespace NCaseFramework.Back.Imp.Combinations
{
    public class CombinationNode : ICombinationNode
    {
        [NotNull] private readonly CombinationId mId;
        [NotNull] private readonly CodeLocation mCodeLocation;
        private readonly bool mIsOnlyPairwise;

        public CombinationNode([NotNull] CombinationId id, [NotNull] CodeLocation codeLocation, bool isOnlyPairwise)
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

        [NotNull] public CombinationId Id
        {
            get { return mId; }
        }

        public CodeLocation CodeLocation
        {
            get { return mCodeLocation; }
        }

        public IEnumerable<INode> Children
        {
            get
            {
                return (Product != null)
                           ? new INode[] {Product}
                           : Enumerable.Empty<INode>();
            }
        }

        public IProdNode Product { get; set; }

        public bool IsOnlyPairwise
        {
            get { return mIsOnlyPairwise; }
        }
    }
}