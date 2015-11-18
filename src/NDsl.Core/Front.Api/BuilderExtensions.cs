using NDsl.Front.Ui;

namespace NDsl.Front.Api
{
    public static class BuilderExtensions
    {
        public static T NewContributor<T>(this CaseBuilder caseBuilder, string name)
        {
            var contributorFactory = caseBuilder.Zapi.Services.GetService<ICreateContributor>();
            return contributorFactory.Create<T>(caseBuilder.Zapi.Model, name);
        }

        public static T NewDefinition<T>(this CaseBuilder caseBuilder, string name) where T : DefBase
        {
            var treeFactory = caseBuilder.Zapi.Services.GetService<IDefFactory<T>>();
            return treeFactory.Create(name, caseBuilder.Zapi.Model.TokenStream);
        }
    }
}