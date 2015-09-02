using System.Collections.Generic;
using NCase.Api;
using NCase.Api.Nod;
using NCase.Api.Vis;
using NDsl.Api.Core;
using NDsl.Api.Core.Nod;
using NDsl.Api.Core.Tok;
using NVisitor.Api.Batch;

namespace NCase.Imp.Vis.TokenStream
{
    public class ParseDirector : Director<IToken, IParseDirector>, IParseDirector
    {
        private readonly Dictionary<ICaseSet, ICaseSetNode> mAllCaseSets;
        private IExtendableNode mCurrentCaseSetNode;

        public ParseDirector(IVisitMapper<IToken, IParseDirector> visitMapper) 
            : base(visitMapper)
        {
            mAllCaseSets = new Dictionary<ICaseSet, ICaseSetNode>();
        }

        public Dictionary<ICaseSet, ICaseSetNode> AllCaseSets
        {
            get { return mAllCaseSets; }
        }

        public IExtendableNode CurrentCaseSetNode
        {
            get { return mCurrentCaseSetNode; }
            set { mCurrentCaseSetNode = value; }
        }
    }
}