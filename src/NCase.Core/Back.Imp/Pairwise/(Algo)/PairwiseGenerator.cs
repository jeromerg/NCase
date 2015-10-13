using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NCase.Back.Api.Pairwise;

namespace NCase.Back.Imp.Pairwise
{
    public class PairwiseGenerator : IPairwiseGenerator
    {
        public IEnumerable<int[]> Generate([NotNull] int[] dimSizes)
        {
            if (dimSizes == null) throw new ArgumentNullException("dimSizes");
            
            var pairGenerations = new List<PairDictionary> { new PairDictionary(dimSizes) };
            while (pairGenerations.First(/*generation*/).Any(/*pair*/))
                yield return GenerateSingleTuple(dimSizes, pairGenerations);
        }

        private int[] GenerateSingleTuple(int[] dimSizes, List<PairDictionary> pairGenerations)
        {
            int[] result = new int[dimSizes.Length];
            var remainingDims = new HashSet<int>(Enumerable.Range(0, dimSizes.Length));

            while (remainingDims.Any())
            {
                int dim1, val1, dim2, val2;
                TakeMostUnused(pairGenerations, remainingDims, out dim1, out val1, out dim2, out val2);
                remainingDims.Remove(dim1);
                remainingDims.Remove(dim2);
                result[dim1] = val1;
                result[dim2] = val2;
            }
            return result;
        }

        private void TakeMostUnused(List<PairDictionary> pairGenerations, HashSet<int> remainingDims, out int dim1, out int val1, out int dim2, out int val2)
        {
            for (int generationIndex = 0; generationIndex < pairGenerations.Count; generationIndex++)
            {
                PairDictionary pairGeneration = pairGenerations[generationIndex];

                bool removed = pairGeneration.TryRemoveFirst(dim1, out val1, out dim2, out val2);
                if (removed)
                {
                    if (generationIndex >= pairGenerations.Count)
                        pairGenerations.Add(new PairDictionary());

                    pairGenerations[generationIndex + 1].Add(dim1, val1, dim2, val2);
                    return;
                }
            }
            throw new ArgumentException();
        }
    }
}
