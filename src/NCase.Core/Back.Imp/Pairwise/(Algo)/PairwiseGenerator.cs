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
            if (dimSizes.Length < 2)
                throw new ArgumentOutOfRangeException("dimSizes", "dimSizes.length must be greater or equal to 2");
            if (dimSizes.Any(s => s <= 0)) throw new ArgumentOutOfRangeException("dimSizes", "Some dimension has no value");

            var pairGenerations = new List<PairDictionary> {new PairDictionary(dimSizes)};

            // while any pair exists in first generation (unused pair)
            while (pairGenerations.First().Any())
                yield return GenerateNextTuple(dimSizes, pairGenerations);
        }

        private int[] GenerateNextTuple(int[] dimSizes, List<PairDictionary> pairGenerations)
        {
            int[] tupleUnderConstruction = Enumerable.Repeat(-1, dimSizes.Length).ToArray();

            var remainingDims = new HashSet<int>(Enumerable.Range(0, dimSizes.Length));

            while (remainingDims.Any())
            {
                int dim1, val1, dim2, val2;
                RemoveLessUsedPair(pairGenerations, remainingDims.ToList(), tupleUnderConstruction, out dim1, out val1, out dim2, out val2);
                remainingDims.Remove(dim1);
                remainingDims.Remove(dim2);
                tupleUnderConstruction[dim1] = val1;
                tupleUnderConstruction[dim2] = val2;
            }
            return tupleUnderConstruction;
        }

        private void RemoveLessUsedPair(
            List<PairDictionary> generations,
            List<int> remainingDims,
            int[] tupleUnderConstruction,
            out int dim1,
            out int val1,
            out int dim2,
            out int val2)
        {
            for (int generationIndex = 0; generationIndex < generations.Count; generationIndex++)
            {
                PairDictionary pairGeneration = generations[generationIndex];

                bool ok = TryRemoveSomePair(pairGeneration, remainingDims.ToList(), out dim1, out val1, out dim2, out val2);
                if (ok)
                {
                    // perfect, pair found => move pair to next generation and returns it
                    PairDictionary nextGeneration = GetOrCreateNextGeneration(generations, generationIndex);
                    nextGeneration.Add(dim1, val1, dim2, val2);
                    return;
                }

                ok = TryRemoveSomePairWithAlreadyFrozenDims(pairGeneration,
                                                           remainingDims,
                                                           tupleUnderConstruction,
                                                           out dim1,
                                                           out val1,
                                                           out dim2,
                                                           out val2);
                if (ok)
                    return;
            }
            throw new ArgumentException("This case should never happen");
        }

        private bool TryRemoveSomePair(PairDictionary generation,
                                       List<int> remainingDims,
                                       out int dim1,
                                       out int val1,
                                       out int dim2,
                                       out int val2)
        {
            // for each possible pair of dimensions (dim1, dim2) !inside! remainingDims 
            // lookup whether the generation contains a pair
            for (int i = 0; i < remainingDims.Count; i++)
            {
                dim1 = remainingDims[i];

                for (int j = i + 1; j < remainingDims.Count; j++)
                {
                    dim2 = remainingDims[j];

                    bool ok = generation.TryRemoveFirst(dim1, out val1, dim2, out val2);
                    if (ok)
                        return true;
                }
            }

            dim1 = val1 = dim2 = val2 = -1;
            return false;
        }

        private bool TryRemoveSomePairWithAlreadyFrozenDims(PairDictionary generation,
                                                           List<int> remainingDims,
                                                           int[] tupleUnderConstruction,
                                                           out int dim1,
                                                           out int val1,
                                                           out int dim2,
                                                           out int val2)
        {
            // for each possible pair of dimensions (dim1, dim2) !inside! remainingDims 
            // lookup whether the generation contains a pair
            for (int i = 0; i < remainingDims.Count; i++)
            {
                dim1 = remainingDims[i];

                for (dim2 = 0; dim2 < tupleUnderConstruction.Length; dim2++)
                {
                    val2 = tupleUnderConstruction[dim2];

                    if (val2 < 0)
                        continue; // dimension is not frozen

                    bool ok = generation.TryRemoveFirst(dim2, val2, dim1, out val1);
                    if (ok)
                        return true;
                }
            }

            dim1 = val1 = dim2 = val2 = -1;
            return false;
        }

        private static PairDictionary GetOrCreateNextGeneration(List<PairDictionary> generations, int generationIndex)
        {
            PairDictionary newGeneration;
            if (generationIndex == generations.Count - 1)
            {
                newGeneration = new PairDictionary();
                generations.Add(newGeneration);
            }
            else
            {
                newGeneration = generations[generationIndex + 1];
            }
            return newGeneration;
        }
    }
}