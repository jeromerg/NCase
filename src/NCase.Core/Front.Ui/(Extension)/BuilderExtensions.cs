using NCase.Front.Imp;

namespace NCase.Front.Ui
{
    public static class BuilderExtensions
    {
        public static T CreateContributor<T>(this IBuilder builder, string name)
        {
            var contributorFactory = builder.Api.ToolBox.GetTool<IContributorFactory>();
            return contributorFactory.Create<T>(builder.Api, name);
        }

        public static ITree CreateTree(this IBuilder builder, string name)
        {
            var treeFactory = builder.Api.ToolBox.GetTool<ITreeFactory>();
            return treeFactory.Create(name, builder.Api.Book);
        }

        public static IProd CreateProd(this IBuilder builder, string name)
        {
            var treeFactory = builder.Api.ToolBox.GetTool<IProdFactory>();
            return treeFactory.Create(name, builder.Api.Book);
        }

        public static ISeq CreateSeq(this IBuilder builder, string name)
        {
            var treeFactory = builder.Api.ToolBox.GetTool<ISeqFactory>();
            return treeFactory.Create(name, builder.Api.Book);
        }

        public static IPairwise CreatePairwise(this IBuilder builder, string name)
        {
            var treeFactory = builder.Api.ToolBox.GetTool<IPairwiseFactory>();
            return treeFactory.Create(name, builder.Api.Book);
        }
    }
}