using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NCase.Back.Api.Core;
using NCase.Front.Api;
using NDsl.Api.Core;
using NDsl.Api.RecPlay;

namespace NCase.Front.Imp
{
    public class Builder : IBuilder
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

        public T CreateContributor<T>(string name)
        {
            if (name == null) throw new ArgumentNullException("name");

            return mInterfaceRecPlayContributorFactory.CreateContributor<T>(mTokenStream, name);
        }

        public TDef CreateDef<TDef>(string name) where TDef : IDef<TDef>
        {
            if (name == null) throw new ArgumentNullException("name");

            IDefFactory defFactory;
            if (!mDefFactories.TryGetValue(typeof (TDef), out defFactory))
                throw new ArgumentException(string.Format(@"No factory found for definition type {0}", typeof (TDef).Name));

            var genericDefFactory = defFactory as IDefFactory<TDef>;
            if (genericDefFactory == null)
                throw new ArgumentException(string.Format(@"Factory for definition type {0} does not implement IDefFactory<{0}>",
                                                          typeof (TDef).Name));

            return genericDefFactory.Create(mTokenStream, name);
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