using System.Collections.Generic;
using NCase.Api.Vis;
using NDsl.Api.Core.Nod;
using NVisitor.Api.Batch;

namespace NCase.Imp.Vis.TreeCaseSet
{
    public class TreeCaseSetInsertChildDirector 
        : Director<INode, ITreeCaseSetInsertChildDirector>
        , ITreeCaseSetInsertChildDirector
    {
        private ITreeCaseSetNode mRoot;

        public TreeCaseSetInsertChildDirector(IEnumerable<IVisitorClass<INode, ITreeCaseSetInsertChildDirector>> visitors)
            : base(visitors)
        {
        }

        public void InitializeCurrentParentCandidate(ITreeCaseSetNode candidate)
        {
            mRoot = candidate;
        }

        public ITreeCaseSetNode Root
        {
            get { return mRoot; }
        }
    }
}