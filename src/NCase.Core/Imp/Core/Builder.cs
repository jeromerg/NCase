using System;
using System.Collections.Generic;
using System.Linq;
using NCase.Api.Dev.Core;
using NCase.Api.Pub;
using NDsl.Api.Dev.Core;
using NDsl.Api.Dev.RecPlay;
using NVisitor.Common.Quality;

namespace NCase.Imp.Core
{
    public class Builder : IBuilder, IBuilderAdvanced
    {
        [NotNull] private readonly ITokenReaderWriter mTokenStream;
        [NotNull] private readonly Dictionary<Type, IDefFactory> mDefFactories;
        [NotNull] private readonly IInterfaceRecPlayContributorFactory mInterfaceRecPlayContributorFactory;
        [NotNull] private readonly IResolver mResolver;

        public Builder([NotNull] IInterfaceRecPlayContributorFactory interfaceRecPlayContributorFactory,
                       [NotNull] ITokenReaderWriter tokenStream,
                       [NotNull] IEnumerable<IDefFactory> defFactories,
                       [NotNull] IResolver resolver)
        {
            if (interfaceRecPlayContributorFactory == null) throw new ArgumentNullException("interfaceRecPlayContributorFactory");
            if (tokenStream == null) throw new ArgumentNullException("tokenStream");
            if (defFactories == null) throw new ArgumentNullException("defFactories");
            if (resolver == null) throw new ArgumentNullException("resolver");

            mTokenStream = tokenStream;
            mDefFactories = defFactories.ToDictionary(f => GetDefType(f));
            mInterfaceRecPlayContributorFactory = interfaceRecPlayContributorFactory;
            mResolver = resolver;
        }

        public IBuilderAdvanced Advanced
        {
            get { return this; }
        }

        public T CreateContributor<T>(string name)
        {
            if (name == null) throw new ArgumentNullException("name");

            return mInterfaceRecPlayContributorFactory.CreateContributor<T>(mTokenStream, name);
        }

        public T CreateDef<T>(string name) where T : IDef
        {
            if (name == null) throw new ArgumentNullException("name");

            IDefFactory defFactory;
            if (!mDefFactories.TryGetValue(typeof (T), out defFactory))
                throw new ArgumentException(string.Format(@"No factory found for definition type {0}", typeof (T).Name));

            var genericDefFactory = defFactory as IDefFactory<IDef>;
            if (genericDefFactory == null)
                throw new ArgumentException(string.Format(@"Factory for definition type {0} does not implement IDefFactory<{0}>",
                                                          typeof (T).Name));

            return (T) genericDefFactory.Create(mTokenStream, name);
        }

        T IBuilderAdvanced.Resolve<T>()
        {
            return mResolver.Resolve<T>();
        }

        private Type GetDefType(IDefFactory defFactory)
        {
            Type factoryType = defFactory.GetType();
            Type defType = factoryType
                .GetInterfaces()
                .First(interf => interf.GetGenericTypeDefinition() == typeof (IDefFactory<>))
                .GetGenericArguments()[0];

            return defType;
        }
    }
}