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
          IGenerateCaseVisitor<IProdDimNode>
    {
        [NotNull, ItemNotNull]
        public IEnumerable<List<INode>> Visit([NotNull] IGenerateCasesDirector dir,
                                              [NotNull] IProdDimNode node,
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
        private IEnumerable<List<INode>> ProduceCartesianProductRecursively([NotNull] IGenerateCasesDirector dir,
                                                                            [NotNull, ItemNotNull] List<INode> dimensions,
                                                                            int dimensionIndex,
                                                                            [NotNull] GenerateOptions options)
        {
            bool isLastDim = dimensionIndex == dimensions.Count - 1;
            INode currentDimNode = dimensions[dimensionIndex];

            IEnumerable<List<INode>> casesOfCurrentDim = dir.Visit(currentDimNode, options);

            if (casesOfCurrentDim == null)
                throw new InvalidOperationException(string.Format("Visit of child {0} returned null", currentDimNode));

            foreach (List<INode> caseFacts in casesOfCurrentDim)
            {
                if (caseFacts == null)
                    throw new InvalidOperationException(
                        string.Format("Visit of child {0} returned a case having the list of facts equal to null", currentDimNode));

                if (isLastDim)
                {
                    yield return caseFacts; // end of recursion here
                }
                else
                {
                    // continue recursion and merge facts together
                    foreach (
                        List<INode> subnodes in ProduceCartesianProductRecursively(dir, dimensions, dimensionIndex + 1, options))
                        yield return ListUtil.Concat(caseFacts, subnodes);
                }
            }
        }
    }
}