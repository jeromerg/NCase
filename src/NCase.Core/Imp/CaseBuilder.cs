using System;
using System.Collections.Generic;
using System.Linq;
using NCase.Api;
using NCase.Api.Dev.Core.CaseSet;
using NCase.Api.Dev.Core.GenerateCase;
using NCase.Api.Dev.Core.Parse;
using NCase.Api.Dev.Core.Replay;
using NDsl.Api.Dev.Core;
using NDsl.Api.Dev.Core.Nod;
using NDsl.Api.Dev.Core.Util;
using NDsl.Api.Dev.RecPlay;
using NVisitor.Api.Lazy;
using NVisitor.Common.Quality;

namespace NCase.Imp
{
    public class CaseBuilder : ICaseBuilder
    {
        [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;
        [NotNull] private readonly ITokenReaderWriter mTokenStream;
        [NotNull] private readonly Dictionary<Type, ICaseSetFactory> mCaseSetFactories;

        [NotNull] private readonly Func<IGenerateCaseDirector> mCaseGeneratorFactory;
        [NotNull] private readonly IReplayDirector mRePlayDirector;
        [NotNull] private readonly IParseDirector mParseDirector;
        [NotNull] private readonly IInterfaceRecPlayContributorFactory mInterfaceRecPlayContributorFactory;

        public CaseBuilder(
            [NotNull] ICodeLocationUtil codeLocationUtil,
            [NotNull] IInterfaceRecPlayContributorFactory interfaceRecPlayContributorFactory,
            [NotNull] IReplayDirector rePlayDirector,  // stateless director, no need for factory
            [NotNull] IParseDirector parseDirector,
            [NotNull] Func<IGenerateCaseDirector> caseGeneratorFactory,
            [NotNull] ITokenReaderWriter tokenStream,
            [NotNull] IEnumerable<ICaseSetFactory> caseSetFactories)
        {
            if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");
            if (interfaceRecPlayContributorFactory == null) throw new ArgumentNullException("interfaceRecPlayContributorFactory");
            if (rePlayDirector == null) throw new ArgumentNullException("rePlayDirector");
            if (parseDirector == null) throw new ArgumentNullException("parseDirector");
            if (caseGeneratorFactory == null) throw new ArgumentNullException("caseGeneratorFactory");
            if (tokenStream == null) throw new ArgumentNullException("tokenStream");
            if (caseSetFactories == null) throw new ArgumentNullException("caseSetFactories");

            mCaseGeneratorFactory = caseGeneratorFactory;
            mTokenStream = tokenStream;
            mCodeLocationUtil = codeLocationUtil;
            mCaseSetFactories = caseSetFactories.ToDictionary(f => GetCaseSetType(f));
            mRePlayDirector = rePlayDirector;
            mParseDirector = parseDirector;
            mInterfaceRecPlayContributorFactory = interfaceRecPlayContributorFactory;
        }

        private Type GetCaseSetType(ICaseSetFactory caseSetFactory)
        {
            Type factoryType = caseSetFactory.GetType();
            Type caseSetType = factoryType
                .GetInterfaces()
                .First(interf => interf.GetGenericTypeDefinition() == typeof (ICaseSetFactory<>))
                .GetGenericArguments()[0];

            return caseSetType;
        }

        public T GetContributor<T>(string name)
        {
            if (name == null) throw new ArgumentNullException("name");

            return mInterfaceRecPlayContributorFactory.CreateContributor<T>(mTokenStream, name);
        }

        public T CreateSet<T>(string name) where T : ICaseSet
        {
            if (name == null) throw new ArgumentNullException("name");

            ICaseSetFactory caseSetFactory;
            if(!mCaseSetFactories.TryGetValue(typeof(T), out caseSetFactory))
                throw new ArgumentException(string.Format(@"No factory found for case set type {0}", typeof(T).Name));

            var genericCaseSetFactory = caseSetFactory as ICaseSetFactory<ICaseSet>;
            if(genericCaseSetFactory == null)
                throw new ArgumentException(string.Format(@"Factory for case set type {0} does not implement ICaseSetFactory<{0}>", typeof(T).Name));

            return (T) genericCaseSetFactory.Create(mTokenStream, name);
        }

        public IEnumerable<Pause> GetAllCases(ICaseSet caseSet)
        {
            // PARSE
            foreach (var token in mTokenStream.Tokens)
                mParseDirector.Visit(token);

            // GENERATE CASES
            IGenerateCaseDirector generateCaseDirector = mCaseGeneratorFactory();
            foreach (List<INode> testCaseNodes in generateCaseDirector.Visit(mParseDirector.GetReference<INode>(caseSet, mCodeLocationUtil.GetCurrentUserCodeLocation())))
            {

                // REPLAY CASE
                foreach (var node in testCaseNodes)
                    mRePlayDirector.Visit(node, true);

                // enable caller to something after having replayed case
                yield return Pause.Now; 

                foreach (var node in testCaseNodes)
                    mRePlayDirector.Visit(node, false);

            } 

        }

    }
}