using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NUtil.Linq;

namespace NUtil.Math.Combinatorics.Pairwise
{
    public class PairwiseGenerator : IPairwiseGenerator
    {
        public IEnumerable<int[]> Generate(int[] dimSizes)
        {
            if (dimSizes == null)
                throw new ArgumentNullException("dimSizes");

            if (dimSizes.Length < 2)
                throw new ArgumentOutOfRangeException("dimSizes", "dimSizes.length must be greater or equal to 2");

            if (dimSizes.Any(s => s <= 0))
                throw new ArgumentOutOfRangeException("dimSizes", "Some dimension has no value");

            var pairGenerations = new List<PairSet> {new PairSet(dimSizes)};

            // while any pair exists in first generation (unused pair)
            while (pairGenerations.First().Any())
                yield return GenerateNextTuple(dimSizes, pairGenerations);
        }

        private int[] GenerateNextTuple(int[] dimSizes, List<PairSet> pairGenerations)
        {
            var tuple = new Tuple(dimSizes.Length);

            while (tuple.FreeDims.Any())
                FreezeOneOrTwoDims(pairGenerations, tuple);

            return tuple.Result;
        }

        private void FreezeOneOrTwoDims(List<PairSet> generations, Tuple tuple)
        {
            for (int generationIndex = 0; generationIndex < generations.Count; generationIndex++)
            {
                PairSet pairGeneration = generations[generationIndex];

                Pair pair = TryPeakBestPair(pairGeneration, tuple);
                if (pair == null)
                    continue;

                // Get next generation 
                PairSet nextGeneration = GetOrCreateNextGeneration(generations, generationIndex);

                // move pair to next generation
                pairGeneration.Remove(pair);
                nextGeneration.Add(pair);

                // remove all pair built with the one or two new dimValues to next generation
                foreach (DimValue dimValue in tuple.FrozenDimValues)
                {
                    var p1 = new Pair(dimValue.Dim, dimValue.Val, pair.Dim1, pair.Val1);
                    var p2 = new Pair(dimValue.Dim, dimValue.Val, pair.Dim2, pair.Val2);
                    pairGeneration.Remove(p1);
                    pairGeneration.Remove(p2);
                    nextGeneration.Add(p1);
                    nextGeneration.Add(p2);
                }

                // add the one or two new dimValues
                tuple.Add(pair.Dim1, pair.Val1);
                tuple.Add(pair.Dim2, pair.Val2);
                return;
            }
            throw new ArgumentException("This case should never happen");
        }

        [CanBeNull]
        private Pair TryPeakBestPair(PairSet pairs, Tuple tuple)
        {
            return TryPeakPairInFreeDims(pairs, tuple)
                   ?? TryPeakPairInFreeAndFrozenDims(pairs, tuple);
        }

        [CanBeNull]
        private Pair TryPeakPairInFreeDims(PairSet pairs, Tuple tuple)
        {
            return tuple.FreeDims
                        .TriangleUnequal((dim1, dim2) => pairs.FirstOrDefault(dim1, dim2))
                        .FirstOrDefault(pair => pair != null);
        }

        [CanBeNull]
        private Pair TryPeakPairInFreeAndFrozenDims(PairSet pairs, Tuple tuple)
        {
            return tuple.FreeDims.CartesianProduct(tuple.FrozenDimValues,
                                                   (dim1, val1) => pairs.FirstOrDefault(val1.Dim, val1.Val, dim1))
                        .FirstOrDefault(pair => pair != null);
        }

        private static PairSet GetOrCreateNextGeneration(List<PairSet> generations, int generationIndex)
        {
            PairSet newGeneration;
            if (generationIndex == generations.Count - 1)
            {
                newGeneration = new PairSet();
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