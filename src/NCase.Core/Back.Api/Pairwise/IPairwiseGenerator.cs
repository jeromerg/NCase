using System.Collections.Generic;
using JetBrains.Annotations;

namespace NCaseFramework.Back.Api.Pairwise
{
    public interface IPairwiseGenerator
    {
        IEnumerable<int[]> Generate([NotNull] int[] dimSizes);
    }
}