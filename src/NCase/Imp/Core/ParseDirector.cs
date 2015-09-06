using System.Collections.Generic;
using NCase.Api.Dev;
using NDsl.Api.Core.Tok;
using NVisitor.Api.Batch;

namespace NCase.Imp.Core
{
    public class ParseDirector : Director<IToken, IParseDirector>, IParseDirector
    {
        private readonly Dictionary<ICaseSet, ICaseSetNode<ICaseSet>> mAllCaseSets;
        private ICaseSetNode mCurrentCaseSet;

        public ParseDirector(IVisitMapper<IToken, IParseDirector> visitMapper) 
            : base(visitMapper)
        {
            mAllCaseSets = new Dictionary<ICaseSet, ICaseSetNode<ICaseSet>>();
        }

        public Dictionary<ICaseSet, ICaseSetNode<ICaseSet>> AllCaseSets
        {
            get { return mAllCaseSets; }
        }

        public ICaseSetNode CurrentCaseSet
        {
            get { return mCurrentCaseSet; }
            set { mCurrentCaseSet = value; }
        }
    }
}