using System.Collections.Generic;
using System.Linq;
using NCase.Api.Nod;
using NCase.Api.Vis;
using NDsl.Api.Core.Nod;
using NVisitor.Api.Lazy;

namespace NCase.Imp.Vis.CaseSets
{
    public class GenerateCaseVisitors
        : ILazyVisitor<INode, IGenerateCaseDirector, ICaseTreeSetNode>
        , ILazyVisitor<INode, IGenerateCaseDirector, ICaseTreeBranchNode>
        , ILazyVisitor<INode, IGenerateCaseDirector, IRefNode<ICaseTreeSetNode>>
    {
        public IEnumerable<Pause> Visit(IGenerateCaseDirector director, ICaseTreeSetNode node)
        {
            foreach (INode child in node.Children)
                foreach (Pause pause in director.Visit(child))
                    yield return pause;
        }

        public IEnumerable<Pause> Visit(IGenerateCaseDirector director, ICaseTreeBranchNode node)
        {
            using (director.Push(node.Fact))
            {
                // it's a leaf, so it is also a case: 
                // then give hand to calling foreach in order to process the case
                if (!node.Branches.Any())
                    yield return Pause.Now;
                else                    
                    foreach (INode child in node.Branches)
                        foreach (Pause pause in director.Visit(child))
                            yield return Pause.Now;
            }
        }

        public IEnumerable<Pause> Visit(IGenerateCaseDirector director, IRefNode<ICaseTreeSetNode> node)
        {
            foreach(Pause pause in director.Visit(node.Reference))
                yield return Pause.Now;
        }

    }
}
