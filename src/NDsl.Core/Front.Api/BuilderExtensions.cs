namespace NDsl.Front.Api
{
    public static class BuilderExtensions
    {
        public static T NewContributor<T>(this IBuilder builder, string name)
        {
            var contributorFactory = builder.Api.Services.GetService<ICreateContributor>();
            return contributorFactory.Create<T>(builder.Api.Model, name);
        }

        public static T NewDefinition<T>(this IBuilder builder, string name) where T : DefBase
        {
            var treeFactory = builder.Api.Services.GetService<IDefFactory<T>>();
            return treeFactory.Create(name, builder.Api.Model.TokenStream);
        }
    }
}