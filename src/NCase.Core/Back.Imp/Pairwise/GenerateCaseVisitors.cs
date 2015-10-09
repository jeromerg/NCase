using System.Collections.Generic;
using System.Linq;
using NCase.Back.Api.Pairwise;
using NCase.Back.Api.Parse;
using NDsl.Back.Api.Common;

namespace NCase.Back.Imp.Pairwise
{
    public class GenerateCaseVisitors
        : IGenerateCaseVisitor<IPairwiseNode>,
          IGenerateCaseVisitor<IPairwiseDimNode>
    {
        public IEnumerable<List<INode>> Visit(IGenerateCasesDirector dir, IPairwiseDimNode node, GenerateOptions options)
        {
            foreach (INode child in node.Children)
                foreach (List<INode> nodes in dir.Visit(child, options))
                    yield return nodes;
        }

        public IEnumerable<List<INode>> Visit(IGenerateCasesDirector dir, IPairwiseNode node, GenerateOptions options)
        {
            List<INode> operands = node.Children.ToList();

            if (operands.Count == 0)
                yield break;
        }
    }
}