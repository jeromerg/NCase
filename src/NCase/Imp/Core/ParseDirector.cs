using System.Collections.Generic;
using NCase.Api.Dev;
using NCase.Imp.Tree;
using NDsl.Api.Core.Tok;
using NVisitor.Api.Batch;

namespace NCase.Imp.Core
{
    public class ParseDirector : Director<IToken, IParseDirector>, IParseDirector
    {
        private readonly Dictionary<ICaseSet, ICaseTreeNode> mAllCaseSets;
        private ICaseTreeNode mCurrentSetNode;

        public ParseDirector(IVisitMapper<IToken, IParseDirector> visitMapper) 
            : base(visitMapper)
        {
            mAllCaseSets = new Dictionary<ICaseSet, ICaseTreeNode>();
        }

        public Dictionary<ICaseSet, ICaseTreeNode> AllCaseSets
        {
            get { return mAllCaseSets; }
        }

        public ICaseTreeNode CurrentSetNode
        {
            get { return mCurrentSetNode; }
            set { mCurrentSetNode = value; }
        }
    }
}