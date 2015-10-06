using JetBrains.Annotations;
using NDsl.Back.Api.Core;

namespace NCase.Back.Api.Pairwise
{
    public class PairwiseId : DefId
    {
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