using System.Collections.Generic;
using System.Linq;
using NCase.Api.Dev.Core.GenerateCase;
using NCase.Api.Dev.Tree;
using NDsl.Api.Dev.Core.Nod;
using NVisitor.Api.Lazy;

namespace NCase.Imp.Tree
{
    public class GenerateCaseVisitors
        : IGenerateCaseVisitor<ITreeNode>
        , IGenerateCaseVisitor<IRefNode<ITreeNode>>
    {
        public IEnumerable<Pause> Visit(IGenerateCaseDirector director, ITreeNode node)
        {
            if (node.Fact != null)
            {
                foreach (var pause in director.Visit(node.Fact)) // fact comes first
                    foreach (Pause pause1 in VisitTreeNodeChildren(director, node))
                        yield return Pause.Now;
            }
            else
            {
                foreach (Pause pause in VisitTreeNodeChildren(director, node))
                    yield return Pause.Now;
            }
        }

        private IEnumerable<Pause> VisitTreeNodeChildren(IGenerateCaseDirector director, ITreeNode node)
        {
            // it's a leaf, so it is also a case: 
            // then give hand to calling foreach in order to process the case
            if (!node.Branches.Any())
                yield return Pause.Now;
            else
                foreach (INode branch in node.Branches)
                    foreach (Pause pause in director.Visit(branch))
                        yield return Pause.Now;

        }

        public IEnumerable<Pause> Visit(IGenerateCaseDirector director, IRefNode<ITreeNode> node)
        {
            foreach(Pause pause in director.Visit(node.Reference))
                yield return Pause.Now;
        }

    }
}
