using System.Collections.Generic;
using System.Linq;
using NCase.Api.Nod;
using NCase.Api.Vis;
using NDsl.Api.Core;
using NVisitor.Api.Lazy;

namespace NCase.Imp.Vis
{
    public class CaseGeneratorVisitors
        : ILazyVisitor<INode, ICaseGeneratorDirector, ICaseSetNode>
        , ILazyVisitor<INode, ICaseGeneratorDirector, ICaseBranchNode>
    {
        public IEnumerable<Pause> Visit(ICaseGeneratorDirector director, ICaseSetNode node)
        {
            foreach (INode child in node.Children)
                foreach (Pause pause in director.Visit(child))
                    yield return pause;
        }

        public IEnumerable<Pause> Visit(ICaseGeneratorDirector director, ICaseBranchNode node)
        {
            using (director.Push(node.CaseFact))
            {
                // it's a leaf, so it is also a case: 
                // then give hand to calling foreach in order to process the case
                if (!node.SubBranches.Any())
                    yield return Pause.Now;
                else                    
                    foreach (INode child in node.SubBranches)
                        foreach (Pause pause in director.Visit(child))
                            yield return pause;
            }
        }

    }
}
