using JetBrains.Annotations;

namespace NCaseFramework.Back.Api.CombinationSet
{
    public class PairwiseProdId : ProdId
    {
        public PairwiseProdId()
        {
        }

        public PairwiseProdId([NotNull] string name)
            : base(name)
        {
        }

        public override string TypeName
        {
            get { return "Pairwise Product"; }
        }
    }
}