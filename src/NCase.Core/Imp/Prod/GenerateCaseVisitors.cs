using System.Collections.Generic;
using System.Linq;
using NCase.Api.Dev.Core.Parse;
using NCase.Api.Dev.Prod;
using NCase.Imp.Helper;
using NDsl.Api.Dev.Core.Nod;

namespace NCase.Imp.Prod
{
    public class GenerateCaseVisitors
        : IGenerateCaseVisitor<IProdNode>
        , IGenerateCaseVisitor<IRefNode<IProdNode>>
        , IGenerateCaseVisitor<ProdDimNode>
    {
        public IEnumerable<List<INode>> Visit(IGenerateDirector dir, IProdNode node)
        {
            List<INode> operands = node.Children.ToList();

            if(operands.Count == 0)
                return Enumerable.Empty<List<INode>>();
            else
                return ProduceCartesianProductRecursively(dir, operands, 0);
        }

        private IEnumerable<List<INode>> ProduceCartesianProductRecursively(IGenerateDirector dir, 
                                                                            List<INode> operands, 
                                                                            int operandIndex)
        {
            bool isLastOperand = operandIndex == operands.Count - 1;
            INode currentOperand = operands[operandIndex];

            foreach (List<INode> nodes in dir.Visit(currentOperand))
            {
                if (isLastOperand)
                {
                    yield return nodes; // end of recursion here
                }
                else
                {
                    // continue recursion
                    foreach (List<INode> subnodes in ProduceCartesianProductRecursively(dir, operands, operandIndex + 1))
                        yield return ListUtil.Concat(nodes, subnodes);
                }
            }

        }

        public IEnumerable<List<INode>> Visit(IGenerateDirector director, IRefNode<IProdNode> node)
        {
            return director.Visit(node.Reference);
        }

        public IEnumerable<List<INode>> Visit(IGenerateDirector director, ProdDimNode node)
        {
            foreach(INode child in node.Children)
                foreach (List<INode> nodes in director.Visit(child))
                    yield return nodes;
        }
    }
}