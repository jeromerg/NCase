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
            mInterfaceRecPlayContributorFactory = interfaceRecPlayContributorFactory;
        }

        [NotNull]
        public T Create<T>([NotNull] ICaseBuilderModel caseBuilderModel, [NotNull] string name)
        {
            if (caseBuilderModel == null) throw new ArgumentNullException("caseBuilderModel");
            if (name == null) throw new ArgumentNullException("name");

            return mInterfaceRecPlayContributorFactory.CreateContributor<T>(caseBuilderModel.TokenStream, name);
        }
    }
}