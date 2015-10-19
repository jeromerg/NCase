using System.Collections.Generic;
using JetBrains.Annotations;

namespace NCase.Back.Api.Pairwise
{
    public interface IPairwiseGenerator
    {
        IEnumerable<int[]> Generate([NotNull] int[] dimSizes);
    }
}