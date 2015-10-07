namespace NCase.Front.Ui
{
    public static class BuilderExtensions
    {
        public static ITree CreateTree(this IBuilder builder, string name)
        {
            return builder.Perform<CreateTree, ITree>(new CreateTree(name));
        }

        public static IProd CreateProd(this IBuilder builder, string name)
        {
            return builder.Perform<CreateProd, IProd>(new CreateProd(name));
        }
        
        public static ISeq CreateSeq(this IBuilder builder, string name)
        {
            return builder.Perform<CreateSeq, ISeq>(new CreateSeq(name));
        }
        
        public static IPairwise CreatePairwise(this IBuilder builder, string name)
        {
            return builder.Perform<CreatePairwise, IPairwise>(new CreatePairwise(name));
        }
    }
}