using System;
using System.Collections.Generic;
using System.Linq;
using NCase.Api;
using NCase.Api.Dev;
using NCase.Imp.Core;
using NDsl.Api.Core.Ex;
using NDsl.Api.Core.Nod;
using NDsl.Api.Core.Util;
using NVisitor.Common.Quality;

namespace NCase.Imp.Prod
{
    public class ProdNode : IProdNode
    {
        [NotNull] private readonly ICodeLocation mCodeLocation;
        [NotNull] private readonly List<INode> mOperands = new List<INode>();
        [NotNull] private readonly IGetBranchingKeyDirector mGetBranchingKeyDirector;

        [CanBeNull] private readonly ProdCaseSet mProd;

        public ProdNode(
            [NotNull] ICodeLocation codeLocation, 
            [NotNull] IGetBranchingKeyDirector getBranchingKeyDirector,
            [CanBeNull] ProdCaseSet prod)
        {
            if (codeLocation == null) throw new ArgumentNullException("codeLocation");
            if (getBranchingKeyDirector == null) throw new ArgumentNullException("getBranchingKeyDirector");

            mCodeLocation = codeLocation;
            mGetBranchingKeyDirector = getBranchingKeyDirector;
            
            mProd = prod;
        }

        public IEnumerable<INode> Children
        {
            get { return mOperands; }
        }

        public ICodeLocation CodeLocation
        {
            get { return mCodeLocation; }
        }

        public IProd CaseSet
        {
            get { return mProd; }
        }

        public void PlaceNextNode(INode child)
        {
            // try to merge with previous ProdDimNode operand, else add new ProdDimNode

            object childBranchingKey = GetBranchingKey(child);
            if(childBranchingKey == null)
            {
                mOperands.Add(child); // no logic: simply add as operand (node manage itself in generate case visitor)
                return;
            }

            ProdDimNode lastOperandAsProdDim = mOperands.LastOrDefault() as ProdDimNode;

            if (lastOperandAsProdDim == null || !Equals(GetBranchingKey(lastOperandAsProdDim), childBranchingKey))
            {
                var prodDim = new ProdDimNode(child);
                mOperands.Add(prodDim);
            }
            else
            {
                lastOperandAsProdDim.PlaceNextNode(child);
            }
        }

        private object GetBranchingKey(INode child)
        {
            object childBranchingKey;
            {
                mGetBranchingKeyDirector.BranchingKey = null;
                mGetBranchingKeyDirector.Visit(child);
                childBranchingKey = mGetBranchingKeyDirector.BranchingKey;
            }
            return childBranchingKey;
        }
    }
}