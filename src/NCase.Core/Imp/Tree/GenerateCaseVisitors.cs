using System.Collections.Generic;
using System.Linq;
using NCase.Api.Dev.Core.Parse;
using NCase.Api.Dev.Tree;
using NCase.Imp.Helper;
using NCase.Imp.Prod;
using NDsl.Api.Dev.Core.Nod;

namespace NCase.Imp.Tree
{
    public class GenerateCaseVisitors
        : IGenerateCaseVisitor<ITreeNode>
        , IGenerateCaseVisitor<IRefNode<ITreeNode>>
    {
        public IEnumerable<List<INode>> Visit(IGenerateDirector director, ITreeNode node)
        {
            if (node.Fact != null)
            {
                foreach (List<INode> factNodes in director.Visit(node.Fact)) // fact comes first
                    foreach (List<INode> subnodes in VisitTreeNodeChildren(director, node))
                        yield return ListUtil.Concat(factNodes, subnodes);
            }
            else
            {
                foreach (List<INode> nodes in VisitTreeNodeChildren(director, node))
                    yield return nodes;
            }
        }

        private IEnumerable<List<INode>> VisitTreeNodeChildren(IGenerateDirector director, ITreeNode node)
        {
            // it's a leaf, so it is also a case: 
            // then give hand to calling foreach in order to process the case
            if (!node.Branches.Any())
                yield return new List<INode>();
            else
                foreach (INode branch in node.Branches)
                    foreach (List<INode> nodes in director.Visit(branch))
                        yield return nodes;

        }

        public IEnumerable<List<INode>> Visit(IGenerateDirector director, IRefNode<ITreeNode> node)
        {
            return director.Visit(node.Reference);
        }

    }
}
