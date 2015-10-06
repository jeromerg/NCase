using System.Collections.Generic;
using System.Linq;
using NCase.Back.Api.Parse;
using NCase.Back.Api.Tree;
using NCase.Back.Api.Util;
using NDsl.Back.Api.Core;

namespace NCase.Back.Imp.Tree
{
    public class GenerateCaseVisitors
        : IGenerateCaseVisitor<ITreeNode>
    {
        public IEnumerable<List<INode>> Visit(IGenerateCasesDirector dir, ITreeNode node, GenerateOptions options)
        {
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

        private IEnumerable<List<INode>> VisitTreeNodeChildren(IGenerateCasesDirector dir, ITreeNode node, GenerateOptions options)
        {
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