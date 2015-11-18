using System;
using JetBrains.Annotations;
using NDsl.Front.Ui;

namespace NDsl.Front.Api
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

        [NotNull] 
        public static T NewDefinition<T>([NotNull] this CaseBuilder caseBuilder, [NotNull] string name) where T : DefBase
        {
            if (caseBuilder == null) throw new ArgumentNullException("caseBuilder");
            if (name == null) throw new ArgumentNullException("name");

            var treeFactory = caseBuilder.Zapi.Services.GetService<IDefFactory<T>>();
            return treeFactory.Create(name, caseBuilder.Zapi.Model.TokenStream);
        }
    }
}