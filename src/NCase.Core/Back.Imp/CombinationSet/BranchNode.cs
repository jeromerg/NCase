using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.Combinations;
using NDsl.All.Def;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Def;
using NDsl.Back.Api.Util;

namespace NCaseFramework.Back.Imp.Combinations
{
    public class BranchNode : IBranchNode
    {
        [NotNull] private readonly CodeLocation mCodeLocation;
        [NotNull] private readonly BranchId mId;
        [NotNull] private readonly INode mDeclarationNode;
        [CanBeNull] private IProdNode mProductNode;

        public BranchNode([NotNull] BranchId id, [NotNull] CodeLocation codeLocation, [NotNull] INode declarationNode)
        {
            if (id == null) throw new ArgumentNullException("id");
            if (codeLocation == null) throw new ArgumentNullException("codeLocation");
            if (declarationNode == null) throw new ArgumentNullException("declarationNode");

            mCodeLocation = codeLocation;
            mDeclarationNode = declarationNode;
            mId = id;
        }

        public CodeLocation CodeLocation
        {
            get { return mCodeLocation; }
        }

        IDefId IDefNode.Id
        {
            get { return mId; }
        }

        public BranchId Id
        {
            get { return mId; }
        }

        public IEnumerable<INode> Children
        {
            get
            {
                yield return mDeclarationNode;

                if(mProductNode != null)
                    yield return mProductNode;
            }
        }

        public INode Declaration
        {
            get { return mDeclarationNode; }
        }

        public IProdNode Product
        {
            get { return mProductNode; }
            set { mProductNode = value; }
        }
    }
}