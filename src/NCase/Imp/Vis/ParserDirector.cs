using System.Collections.Generic;
using NCase.Api;
using NCase.Api.Nod;
using NCase.Api.Vis;
using NDsl.Api.Core;
using NVisitor.Api.Batch;

namespace NCase.Imp.Vis
{
    public class ParserDirector : Director<IToken, IParserDirector>, IParserDirector
    {
        private readonly Dictionary<ICaseSet, ICaseSetNode> mAllCaseSets;
        private ICaseSetNode mCurrentCaseSetNode;

        public ParserDirector(IVisitMapper<IToken, IParserDirector> visitMapper) 
            : base(visitMapper)
        {
            mAllCaseSets = new Dictionary<ICaseSet, ICaseSetNode>();
        }

        public Dictionary<ICaseSet, ICaseSetNode> AllCaseSets
        {
            get { return mAllCaseSets; }
        }

        public ICaseSetNode CurrentCaseSetNode
        {
            get { return mCurrentCaseSetNode; }
            set { mCurrentCaseSetNode = value; }
        }
    }
}