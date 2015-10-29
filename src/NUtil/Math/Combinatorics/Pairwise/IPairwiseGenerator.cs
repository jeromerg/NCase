using System.Collections.Generic;
using JetBrains.Annotations;

namespace NUtil.Math.Combinatorics.Pairwise
{
    public interface IPairwiseGenerator
    {
        IEnumerable<int[]> Generate([NotNull] int[] dimSizes);
    }
}