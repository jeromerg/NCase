using NDsl.Front.Ui;

namespace NDsl.Front.Api
{
    public static class BuilderExtensions
    {
        public static T NewContributor<T>(this IBuilder builder, string name)
        {
            var contributorFactory = builder.Zapi.Services.GetService<ICreateContributor>();
            return contributorFactory.Create<T>(builder.Zapi.Model, name);
        }

        public static T NewDefinition<T>(this IBuilder builder, string name) where T : DefBase
        {
            var treeFactory = builder.Zapi.Services.GetService<IDefFactory<T>>();
            return treeFactory.Create(name, builder.Zapi.Model.TokenStream);
        }
    }
}