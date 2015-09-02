using System.Collections.Generic;
using NCase.Api.Vis;
using NDsl.Api.Core;
using NDsl.Api.Core.Nod;
using NVisitor.Api.Batch;

namespace NCase.Imp.Vis.TreeCaseSet
{
    public class TreeCaseSetInsertChildDirector 
        : Director<INode, ITreeCaseSetInsertChildDirector>
        , ITreeCaseSetInsertChildDirector
    {
        private INode mCurrentParentCandidate;

        public TreeCaseSetInsertChildDirector(IEnumerable<IVisitorClass<INode, ITreeCaseSetInsertChildDirector>> visitors)
            : base(visitors)
        {
        }

        public void InitializeCurrentParentCandidate(INode candidate)
        {
            mCurrentParentCandidate = candidate;
        }

        public INode CurrentParentCandidate
        {
            get { return mCurrentParentCandidate; }
        }
    }
}