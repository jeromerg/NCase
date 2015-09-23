using System.Collections.Generic;
using System.Linq;
using NCase.Back.Api.Pairwise;
using NCase.Back.Api.Parse;
using NDsl.Back.Api.Core;
using NDsl.Back.Api.Ref;

namespace NCase.Back.Imp.Pairwise
{
    public class GenerateCaseVisitors
        : IGenerateCaseVisitor<IPairwiseNode>,
          IGenerateCaseVisitor<IRefNode<IPairwiseNode>>,
          IGenerateCaseVisitor<IPairwiseDimNode>
    {
        public IEnumerable<List<INode>> Visit(IGenerateDirector director, IPairwiseDimNode node)
        {
            foreach (INode child in node.Children)
                foreach (List<INode> nodes in director.Visit(child))
                    yield return nodes;
        }

        public IEnumerable<List<INode>> Visit(IGenerateDirector dir, IPairwiseNode node)
        {
            List<INode> operands = node.Children.ToList();

            if (operands.Count == 0)
                yield break;
        }

        public IEnumerable<List<INode>> Visit(IGenerateDirector director, IRefNode<IPairwiseNode> node)
        {
            return director.Visit(node.Reference);
        }
    }
}