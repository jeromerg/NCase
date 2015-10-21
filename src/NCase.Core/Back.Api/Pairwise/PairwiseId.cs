using JetBrains.Annotations;
using NCaseFramework.Back.Api.SetDef;

namespace NCaseFramework.Back.Api.Pairwise
{
    public class PairwiseId : SetDefId
    {
        public PairwiseId()
        {
        }

        public PairwiseId([NotNull] string name)
            : base(name)
        {
        }

        public override string DefTypeName
        {
            get { return "Pairwise"; }
        }
    }
}