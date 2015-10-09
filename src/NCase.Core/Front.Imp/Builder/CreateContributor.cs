using System;
using JetBrains.Annotations;
using NCase.Front.Api.Builder;
using NDsl.Back.Api.RecPlay;

namespace NCase.Front.Imp.Builder
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