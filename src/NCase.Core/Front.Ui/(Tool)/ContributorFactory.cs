using System;
using JetBrains.Annotations;
using NCase.Front.Api;
using NDsl.Back.Api.RecPlay;
using NDsl.Front.Ui;

namespace NCase.Front.Ui
{
    public class ContributorFactory : IContributorFactory
    {
        [NotNull] private readonly IInterfaceRecPlayContributorFactory mInterfaceRecPlayContributorFactory;

        public ContributorFactory([NotNull] IInterfaceRecPlayContributorFactory interfaceRecPlayContributorFactory)
        {
            if (interfaceRecPlayContributorFactory == null) throw new ArgumentNullException("interfaceRecPlayContributorFactory");
            mInterfaceRecPlayContributorFactory = interfaceRecPlayContributorFactory;
        }

        public T Create<T>(IBuilderApi builderApi, string name)
        {
            return mInterfaceRecPlayContributorFactory.CreateContributor<T>(builderApi.Book, name);
        }
    }
}