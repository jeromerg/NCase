using System;
using System.Collections.Generic;
using System.Linq;
using NCase.Api;
using NCase.Api.CaseSet;
using NCase.Api.Vis;
using NDsl.Api.RecPlay;
using NDsl.Imp.Core;
using NVisitor.Api.Lazy;
using NVisitor.Common.Quality;

namespace NCase.Imp
{
    public class CaseBuilder : ICaseBuilder
    {
        [NotNull] private readonly ITokenReaderWriter mTokenStream;
        [NotNull] private readonly Dictionary<Type, ICaseSetFactory> mCaseSetFactories;

        [NotNull] private readonly Func<ICaseGeneratorDirector> mCaseGeneratorFactory;
        [NotNull] private readonly IReplayDirector mRePlayDirector;
        [NotNull] private readonly IParserDirector mParserDirector;
        [NotNull] private readonly IInterfaceRecPlayContributorFactory mInterfaceRecPlayContributorFactory;

        public CaseBuilder(
            [NotNull] IInterfaceRecPlayContributorFactory interfaceRecPlayContributorFactory,
            [NotNull] IReplayDirector rePlayDirector,  // stateless director, no need for factory
            [NotNull] IParserDirector parserDirector,
            [NotNull] Func<ICaseGeneratorDirector> caseGeneratorFactory,
            [NotNull] ITokenReaderWriter tokenStream,
            [NotNull] IEnumerable<ICaseSetFactory> caseSetFactories
            )
        {
            if (interfaceRecPlayContributorFactory == null) throw new ArgumentNullException("interfaceRecPlayContributorFactory");
            if (rePlayDirector == null) throw new ArgumentNullException("rePlayDirector");
            if (parserDirector == null) throw new ArgumentNullException("parserDirector");
            if (caseGeneratorFactory == null) throw new ArgumentNullException("caseGeneratorFactory");
            if (tokenStream == null) throw new ArgumentNullException("tokenStream");
            if (caseSetFactories == null) throw new ArgumentNullException("caseSetFactories");

            mCaseGeneratorFactory = caseGeneratorFactory;
            mTokenStream = tokenStream;
            mCaseSetFactories = caseSetFactories.ToDictionary(f => GetCaseSetType(f));
            mRePlayDirector = rePlayDirector;
            mParserDirector = parserDirector;
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
                throw new ArgumentException(@"No factory found for case set type {0}", typeof(T).Name);

            var genericCaseSetFactory = caseSetFactory as ICaseSetFactory<ICaseSet>;
            if(genericCaseSetFactory == null)
                throw new ArgumentException(@"Factory for case set type {0} does not implement ICaseSetFactory<{0}>", typeof(T).Name);

            return (T) genericCaseSetFactory.Create(mTokenStream, name);
        }

        public IEnumerable<Pause> GetAllCases(ICaseSet caseSet)
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