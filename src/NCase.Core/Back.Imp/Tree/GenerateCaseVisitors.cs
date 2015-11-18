using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.Parse;
using NCaseFramework.Back.Api.Tree;
using NCaseFramework.Back.Api.Util;
using NDsl.Back.Api.Common;

namespace NCaseFramework.Back.Imp.Tree
{
    public class GenerateCaseVisitors
        : IGenerateCaseVisitor<ITreeNode>
    {
        [NotNull, ItemNotNull]
        public IEnumerable<List<INode>> Visit([NotNull] IGenerateCasesDirector dir,
                                              [NotNull] ITreeNode node,
                                              [NotNull] GenerateOptions options)
        {
            if (options == null) throw new ArgumentNullException("options");

            if (node.Fact != null)
            {
                foreach (List<INode> factNodes in dir.Visit(node.Fact, options)) // fact comes first
                    foreach (List<INode> subnodes in VisitTreeNodeChildren(dir, node, options))
                        yield return ListUtil.Concat(factNodes, subnodes);
            }
            else
            {
                foreach (List<INode> nodes in VisitTreeNodeChildren(dir, node, options))
                    yield return nodes;
            }
        }

        [NotNull, ItemNotNull]
        private IEnumerable<List<INode>> VisitTreeNodeChildren([NotNull] IGenerateCasesDirector dir,
                                                               [NotNull] ITreeNode node,
                                                               [NotNull] GenerateOptions options)
        {
            if (options == null) throw new ArgumentNullException("options");

            // it's a leaf, so it is also a case: 
            // then give hand to calling foreach in order to process the case
            if (!node.Branches.Any())
                yield return new List<INode>();
            else
                foreach (INode branch in node.Branches)
                    foreach (List<INode> nodes in dir.Visit(branch, options))
                        yield return nodes;
        }
    }
}