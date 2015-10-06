using System.Collections.Generic;
using System.Linq;
using NCase.Back.Api.Parse;
using NCase.Back.Api.Prod;
using NCase.Back.Api.Util;
using NDsl.Back.Api.Core;

namespace NCase.Back.Imp.Prod
{
    public class GenerateCaseVisitors
        : IGenerateCaseVisitor<IProdNode>,
          IGenerateCaseVisitor<ProdDimNode>
    {
        public IEnumerable<List<INode>> Visit(IGenerateCasesDirector dir, IProdNode node, GenerateOptions options)
        {
            List<INode> operands = node.Children.ToList();

            if (operands.Count == 0)
                return Enumerable.Empty<List<INode>>();
            else
                return ProduceCartesianProductRecursively(dir, operands, 0, options);
        }

        public IEnumerable<List<INode>> Visit(IGenerateCasesDirector dir, ProdDimNode node, GenerateOptions options)
        {
            foreach (INode child in node.Children)
                foreach (List<INode> nodes in dir.Visit(child, options))
                    yield return nodes;
        }

        private IEnumerable<List<INode>> ProduceCartesianProductRecursively(IGenerateCasesDirector dir,
                                                                            List<INode> operands,
                                                                            int operandIndex,
                                                                            GenerateOptions options)
        {
            bool isLastOperand = operandIndex == operands.Count - 1;
            INode currentOperand = operands[operandIndex];

            foreach (List<INode> nodes in dir.Visit(currentOperand, options))
            {
                if (isLastOperand)
                {
                    yield return nodes; // end of recursion here
                }
                else
                {
                    // continue recursion
                    foreach (List<INode> subnodes in ProduceCartesianProductRecursively(dir, operands, operandIndex + 1, options))
                        yield return ListUtil.Concat(nodes, subnodes);
                }
            }
        }
    }
}