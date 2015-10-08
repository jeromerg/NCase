using NCase.Front.Imp;

namespace NCase.Front.Ui
{
    public static class BuilderExtensions
    {
        public static ITree CreateTree(this IBuilder builder, string name)
        {
            var treeFactory = builder.Api.Tool<ITreeFactory>();
            return treeFactory.Create(name, builder.Api.Book);
        }

        public static IProd CreateProd(this IBuilder builder, string name)
        {
            var treeFactory = builder.Api.Tool<IProdFactory>();
            return treeFactory.Create(name, builder.Api.Book);
        }

        public static ISeq CreateSeq(this IBuilder builder, string name)
        {
            var treeFactory = builder.Api.Tool<ISeqFactory>();
            return treeFactory.Create(name, builder.Api.Book);
        }

        public static IPairwise CreatePairwise(this IBuilder builder, string name)
        {
            var treeFactory = builder.Api.Tool<IPairwiseFactory>();
            return treeFactory.Create(name, builder.Api.Book);
        }
    }
}