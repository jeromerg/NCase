using System.Collections.Generic;
using NCase.Api.Vis;
using NDsl.Api.Core;
using NVisitor.Api.Batch;

namespace NCase.Imp.Vis
{
    public class TreeCaseSetInsertChildDirector 
        : Director<INode, ITreeCaseSetInsertChildDirector>
        , ITreeCaseSetInsertChildDirector
    {
        private INode mRoot;

        public TreeCaseSetInsertChildDirector(IEnumerable<IVisitorClass<INode, ITreeCaseSetInsertChildDirector>> visitors)
            : base(visitors)
        {
        }

        public void InitializeRoot(INode root)
        {
            mRoot = root;
        }

        public INode Root
        {
            get { return mRoot; }
        }
    }
}