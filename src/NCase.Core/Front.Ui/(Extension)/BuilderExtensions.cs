using NCase.Front.Imp;

namespace NCase.Front.Ui
{
    public static class BuilderExtensions
    {
        public static T CreateContributor<T>(this IBuilder builder, string name)
        {
            var contributorFactory = builder.Api.Services.GetTool<IContributorFactory>();
            return contributorFactory.Create<T>(builder.Api.Model, name);
        }

        public static ITree CreateTree(this IBuilder builder, string name)
        {
            var treeFactory = builder.Api.Services.GetTool<ITreeFactory>();
            return treeFactory.Create(name, builder.Api.Model.Book);
        }

        public static IProd CreateProd(this IBuilder builder, string name)
        {
            var treeFactory = builder.Api.Services.GetTool<IProdFactory>();
            return treeFactory.Create(name, builder.Api.Model.Book);
        }

        public static ISeq CreateSeq(this IBuilder builder, string name)
        {
            var treeFactory = builder.Api.Services.GetTool<ISeqFactory>();
            return treeFactory.Create(name, builder.Api.Model.Book);
        }

        public static IPairwise CreatePairwise(this IBuilder builder, string name)
        {
            var treeFactory = builder.Api.Services.GetTool<IPairwiseFactory>();
            return treeFactory.Create(name, builder.Api.Model.Book);
        }
    }
}