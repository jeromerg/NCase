using JetBrains.Annotations;
using NCase.Back.Api.Tree;

namespace NCase.Back.Api.Pairwise
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