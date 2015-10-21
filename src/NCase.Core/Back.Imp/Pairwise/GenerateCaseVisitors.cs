using System.Collections.Generic;
using System.Linq;
using NCaseFramework.Back.Api.Pairwise;
using NCaseFramework.Back.Api.Parse;
using NDsl.Back.Api.Common;

namespace NCaseFramework.Back.Imp.Pairwise
{
    public class GenerateCaseVisitors
        : IGenerateCaseVisitor<IPairwiseNode>,
          IGenerateCaseVisitor<IPairwiseDimNode>
    {
        private readonly IPairwiseGenerator mPairwiseGenerator;

        public GenerateCaseVisitors(IPairwiseGenerator pairwiseGenerator)
        {
            mPairwiseGenerator = pairwiseGenerator;
        }

        public IEnumerable<List<INode>> Visit(IGenerateCasesDirector dir, IPairwiseDimNode node, GenerateOptions options)
        {
            foreach (INode child in node.Children)
                // TODO: AddRange instead??
                foreach (List<INode> nodes in dir.Visit(child, options))
                    yield return nodes;
        }

        public IEnumerable<List<INode>> Visit(IGenerateCasesDirector dir, IPairwiseNode node, GenerateOptions options)
        {
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