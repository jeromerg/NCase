using System;
using JetBrains.Annotations;
using NDsl.Front.Api;

namespace NDsl.Front.Ui
{
    public static class BuilderExtensions
    {
        [NotNull]
        public static T NewContributor<T>([NotNull] this CaseBuilder caseBuilder, [NotNull] string name)
        {
            if (caseBuilder == null) throw new ArgumentNullException("caseBuilder");
            if (name == null) throw new ArgumentNullException("name");

            var contributorFactory = caseBuilder.Zapi.Services.GetService<ICreateContributor>();
            return contributorFactory.Create<T>(caseBuilder.Zapi.Model, name);
        }
    }
}