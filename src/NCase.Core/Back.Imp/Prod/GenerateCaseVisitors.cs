using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.Parse;
using NCaseFramework.Back.Api.Prod;
using NCaseFramework.Back.Api.Util;
using NDsl.Back.Api.Common;

namespace NCaseFramework.Back.Imp.Prod
{
    public class GenerateCaseVisitors
        : IGenerateCaseVisitor<IProdNode>,
          IGenerateCaseVisitor<ProdDimNode>
    {
        [NotNull, ItemNotNull]
        public IEnumerable<List<INode>> Visit([NotNull] IGenerateCasesDirector dir,
                                              [NotNull] IProdNode node,
                                              [NotNull] GenerateOptions options)
        {
            if (options == null) throw new ArgumentNullException("options");

            List<INode> operands = node.Children.ToList();

            if (operands.Count == 0)
                return Enumerable.Empty<List<INode>>();
            else
                return ProduceCartesianProductRecursively(dir, operands, 0, options);
        }

        [NotNull, ItemNotNull]
        public IEnumerable<List<INode>> Visit([NotNull] IGenerateCasesDirector dir,
                                              [NotNull] ProdDimNode node,
                                              [NotNull] GenerateOptions options)
        {
            if (options == null) throw new ArgumentNullException("options");

            foreach (INode child in node.Children)
                foreach (List<INode> nodes in dir.Visit(child, options))
                    yield return nodes;
        }

        [NotNull, ItemNotNull] 
        private IEnumerable<List<INode>> ProduceCartesianProductRecursively([NotNull] IGenerateCasesDirector dir,
                                                                            [NotNull, ItemNotNull] List<INode> operands, 
                                                                            int operandIndex,
                                                                            [NotNull] GenerateOptions options)
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