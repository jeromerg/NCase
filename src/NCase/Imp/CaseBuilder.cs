using System;
using System.Collections.Generic;
using NCase.Api;
using NCase.Api.Vis;
using NDsl.Api.RecPlay;
using NDsl.Imp.Core;
using NVisitor.Api.Lazy;
using NVisitor.Common.Quality;

namespace NCase.Imp
{
    public class CaseBuilder : ICaseBuilder
    {
        [NotNull] private readonly TokenStream mTokenStream = new TokenStream();
        
        [NotNull] private readonly Func<ICaseGeneratorDirector> mCaseGeneratorFactory;
        [NotNull] private readonly IReplayDirector mRePlayDirector;
        private readonly IParserDirector mParserDirector;
        [NotNull] private readonly IRecPlayContributorFactory mRecPlayContributorFactory;

        public CaseBuilder(
            [NotNull] IRecPlayContributorFactory recPlayContributorFactory,
            [NotNull] IReplayDirector rePlayDirector,  // stateless director, no need for factory
            [NotNull] IParserDirector parserDirector,
            [NotNull] Func<ICaseGeneratorDirector> caseGeneratorFactory
            )
        {
            if (recPlayContributorFactory == null) throw new ArgumentNullException("recPlayContributorFactory");
            if (rePlayDirector == null) throw new ArgumentNullException("rePlayDirector");
            if (parserDirector == null) throw new ArgumentNullException("parserDirector");
            if (caseGeneratorFactory == null) throw new ArgumentNullException("caseGeneratorFactory");

            mCaseGeneratorFactory = caseGeneratorFactory;
            mRePlayDirector = rePlayDirector;
            mParserDirector = parserDirector;
            mRecPlayContributorFactory = recPlayContributorFactory;
        }

        public T GetContributor<T>(string name)
        {
            if (name == null) throw new ArgumentNullException("name");

            return mRecPlayContributorFactory.CreateInterface<T>(mTokenStream, name);
        }

        public CaseSet CreateSet(string name)
        {
            if (name == null) throw new ArgumentNullException("name");
            
            return new CaseSet(mTokenStream, name);
        }

        public IEnumerable<Pause> GetAllCases(CaseSet caseSet)
        {
            // build the caseSet trees => fills the AllCaseSets dictionary
            foreach (var token in mTokenStream.Tokens)
                mParserDirector.Visit(token);

            ICaseGeneratorDirector caseGeneratorDirector = mCaseGeneratorFactory();
            foreach (var pause in caseGeneratorDirector.Visit(mParserDirector.AllCaseSets[caseSet]))
            {

                mRePlayDirector.IsReplay = true;
                foreach (var node in caseGeneratorDirector.CurrentCase)
                    mRePlayDirector.Visit(node);

                // enable caller to something after having replayed case
                yield return Pause.Now; 

                mRePlayDirector.IsReplay = false;
                foreach (var node in caseGeneratorDirector.CurrentCase)
                    mRePlayDirector.Visit(node);

            } 

        }

    }
}