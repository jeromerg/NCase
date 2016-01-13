using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.Pairwise;
using NCaseFramework.Back.Api.Parse;
using NDsl.Back.Api.Common;
using NUtil.Math.Combinatorics.Pairwise;

namespace NCaseFramework.Back.Imp.Pairwise
{
    public class GenerateCaseVisitors
        : IGenerateCaseVisitor<IPairwiseNode>,
          IGenerateCaseVisitor<IPairwiseDimNode>
    {
        [NotNull] private readonly IPairwiseGenerator mPairwiseGenerator;

        public GenerateCaseVisitors([NotNull] IPairwiseGenerator pairwiseGenerator)
        {
            mPairwiseGenerator = pairwiseGenerator;
        }

        [NotNull, ItemNotNull]
        public IEnumerable<List<INode>> Visit([NotNull] IGenerateCasesDirector dir,
                                              [NotNull] IPairwiseDimNode node,
                                              [NotNull] GenerateOptions options)
        {
            if (options == null) throw new ArgumentNullException("options");

            foreach (INode child in node.Children)
            {
                IEnumerable<List<INode>> casesOfChild = dir.Visit(child, options);

                if (casesOfChild == null)
                    throw new InvalidOperationException(string.Format("Visit of child {0} returned null", child));

                foreach (List<INode> caseFacts in casesOfChild)
                    yield return caseFacts;
            }
        }

        [NotNull, ItemNotNull]
        public IEnumerable<List<INode>> Visit([NotNull] IGenerateCasesDirector dir,
                                              [NotNull] IPairwiseNode node,
                                              [NotNull] GenerateOptions options)
        {
            if (options == null) throw new ArgumentNullException("options");

            // COLLECT ALL SUBCASES
            var allDimsCasesFacts = new List<List<List<INode>>>();

            foreach (INode dimNode in node.Children) // foreach dim
            {
                var casesForThisDim = new List<List<INode>>();

                IEnumerable<List<INode>> factsForAllCases = dir.Visit(dimNode, options);

                if (factsForAllCases == null)
                    throw new InvalidOperationException(string.Format("Visit of child {0} returned null", dimNode));

                foreach (List<INode> caseFacts in factsForAllCases)
                    casesForThisDim.Add(caseFacts);

                allDimsCasesFacts.Add(casesForThisDim);
            }

            // GENERATE AND YIELD ALL PAIRWISE TEST CASES
            {
                // ReSharper disable once PossibleNullReferenceException
                int[] dimSizes = allDimsCasesFacts.Select(dimCases => dimCases.Count).ToArray();

                IEnumerable<int[]> pairwiseDimValueIndexes = mPairwiseGenerator.Generate(dimSizes);

                foreach (int[] valueForEachDim in pairwiseDimValueIndexes)
                {
                    var nodes = new List<INode>();
                    for (int dim = 0; dim < valueForEachDim.Length; dim++)
                    {
                        int value = valueForEachDim[dim];
                        // ReSharper disable once PossibleNullReferenceException
                        // ReSharper disable once AssignNullToNotNullAttribute
                        nodes.AddRange(allDimsCasesFacts[dim][value]);
                    }
                    yield return nodes;
                }
            }
        }
    }
}