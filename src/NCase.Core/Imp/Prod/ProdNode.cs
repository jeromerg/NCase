using System;
using System.Collections.Generic;
using NCase.Api;
using NCase.Api.Dev.Prod;
using NCase.Api.Pub;
using NDsl.Api.Dev.Core.Nod;
using NDsl.Api.Dev.Core.Util;
using NVisitor.Common.Quality;

namespace NCase.Imp.Prod
{
    public class ProdNode : IProdNode
    {
        [NotNull] private readonly ICodeLocation mCodeLocation;
        [NotNull] private readonly List<INode> mDimensions = new List<INode>();

        [CanBeNull] private readonly IProd mProd;

        public ProdNode(
            [NotNull] ICodeLocation codeLocation, 
            [CanBeNull] IProd prod)
        {
            if (codeLocation == null) throw new ArgumentNullException("codeLocation");

            mCodeLocation = codeLocation;
            
            mProd = prod;
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

        public IProd CaseSet
        {
            get { return mProd; }
        }
    }
}