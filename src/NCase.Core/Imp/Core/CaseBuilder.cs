using System;
using System.Collections.Generic;
using System.Linq;
using NCase.Api;
using NCase.Api.Dev.Core;
using NDsl.Api.Dev.Core;
using NDsl.Api.Dev.RecPlay;
using NVisitor.Common.Quality;

namespace NCase.Imp.Core
{
    public class CaseBuilder : ICaseBuilder
    {
        [NotNull] private readonly ITokenReaderWriter mTokenStream;
        [NotNull] private readonly Dictionary<Type, ICaseSetFactory> mCaseSetFactories;
        [NotNull] private readonly IInterfaceRecPlayContributorFactory mInterfaceRecPlayContributorFactory;

        public CaseBuilder(
            [NotNull] IInterfaceRecPlayContributorFactory interfaceRecPlayContributorFactory,
            [NotNull] ITokenReaderWriter tokenStream,
            [NotNull] IEnumerable<ICaseSetFactory> caseSetFactories)
        {
            if (interfaceRecPlayContributorFactory == null) throw new ArgumentNullException("interfaceRecPlayContributorFactory");
            if (tokenStream == null) throw new ArgumentNullException("tokenStream");
            if (caseSetFactories == null) throw new ArgumentNullException("caseSetFactories");

            mTokenStream = tokenStream;
            mCaseSetFactories = caseSetFactories.ToDictionary(f => GetCaseSetType(f));
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
    }
}