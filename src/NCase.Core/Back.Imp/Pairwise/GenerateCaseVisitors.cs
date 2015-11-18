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
                // TODO: AddRange instead??
                foreach (List<INode> nodes in dir.Visit(child, options))
                    yield return nodes;
        }

        [NotNull, ItemNotNull]
        public IEnumerable<List<INode>> Visit([NotNull] IGenerateCasesDirector dir,
                                              [NotNull] IPairwiseNode node,
                                              [NotNull] GenerateOptions options)
        {
            if (options == null) throw new ArgumentNullException("options");

            // COLLECT ALL SUBCASES
            var dimsValuesNodes = new List<List<List<INode>>>();
            foreach (INode child in node.Children)
            {
                var valuesNodes = new List<List<INode>>();
                dimsValuesNodes.Add(valuesNodes);
                foreach (List<INode> valueNodes in dir.Visit(child, options))
                    valuesNodes.Add(valueNodes);
            }

            // GENERATE AND YIELD ALL PAIRWISE TEST CASES
            {
                IEnumerable<int[]> valueForEachDimEnumerable =
                    mPairwiseGenerator.Generate(dimsValuesNodes.Select(d => d.Count).ToArray());

                foreach (int[] valueForEachDim in valueForEachDimEnumerable)
                {
                    var nodes = new List<INode>();
                    for (int dim = 0; dim < valueForEachDim.Length; dim++)
                    {
                        int value = valueForEachDim[dim];
                        nodes.AddRange(dimsValuesNodes[dim][value]);
                    }
                    yield return nodes;
                }
            }
        }
    }
}