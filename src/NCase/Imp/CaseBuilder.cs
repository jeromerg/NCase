using System;
using System.Collections.Generic;
using NCase.Api;
using NCase.Api.Vis;
using NDsl.Api.Core;
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
        [NotNull] private readonly IRePlayDirector mRePlayDirector;
        private readonly IParserDirector mParserDirector;
        [NotNull] private readonly IRecPlayContributorFactory mRecPlayContributorFactory;

        public CaseBuilder(
            [NotNull] IRecPlayContributorFactory recPlayContributorFactory,
            [NotNull] IRePlayDirector rePlayDirector,  // stateless director, no need for factory
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

        public IEnumerable<Pause> GetAllCases()
        {
            foreach (var token in mTokenStream.Tokens)
            {
                mParserDirector.Visit(token);
            }

            // TODO continue dev here!
            mTokenStream.State = StreamState.Reading;

            ICaseGeneratorDirector caseGeneratorDirector = mCaseGeneratorFactory();
            foreach (var pause in caseGeneratorDirector.Visit(mTokenStream))
            {
                // TODO: IMPROVE REPLAY BY INTRODUCING VALUE-CLEANING AFTER EACH REPLAY!
                ReplayCase(caseGeneratorDirector.CurrentCase);
                yield return Pause.Now;
            } 

        }

        private void ReplayCase(IEnumerable<INode> nodes)
        {
            foreach (var node in nodes)
                mRePlayDirector.Visit(node);
        }
    }
}