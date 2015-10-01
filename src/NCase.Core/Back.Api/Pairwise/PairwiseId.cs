using JetBrains.Annotations;
using NDsl.Back.Api;

namespace NCase.Back.Api.Pairwise
{
    public class PairwiseId : DefId
    {
        public PairwiseId([NotNull] string name)
            : base(name)
        {
        }
    }
}