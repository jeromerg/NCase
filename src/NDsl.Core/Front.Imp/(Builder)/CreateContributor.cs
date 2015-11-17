using System;
using JetBrains.Annotations;
using NDsl.Back.Api.Builder;
using NDsl.Back.Api.RecPlay;
using NDsl.Front.Api;

namespace NDsl.Front.Imp
{
    public class CreateContributor : ICreateContributor
    {
        [NotNull] private readonly IInterfaceRecPlayContributorFactory mInterfaceRecPlayContributorFactory;

        public CreateContributor([NotNull] IInterfaceRecPlayContributorFactory interfaceRecPlayContributorFactory)
        {
            if (interfaceRecPlayContributorFactory == null) throw new ArgumentNullException("interfaceRecPlayContributorFactory");
            mInterfaceRecPlayContributorFactory = interfaceRecPlayContributorFactory;
        }

        public T Create<T>(IBuilderModel builderModel, string name)
        {
            return mInterfaceRecPlayContributorFactory.CreateContributor<T>(builderModel.TokenStream, name);
        }
    }
}