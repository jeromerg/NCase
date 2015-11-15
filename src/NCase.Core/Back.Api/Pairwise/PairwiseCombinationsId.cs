using JetBrains.Annotations;
using NCaseFramework.Back.Api.SetDef;

namespace NCaseFramework.Back.Api.Pairwise
{
    public class PairwiseCombinationsId : SetDefId
    {
        public PairwiseCombinationsId([NotNull] string name)
            : base(name)
        {
        }

        public override string TypeName
        {
            get { return "PairwiseCombinations"; }
        }
    }
}