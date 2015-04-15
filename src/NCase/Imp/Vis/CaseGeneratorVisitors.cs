using System.Collections.Generic;
using System.Linq;
using NCase.Api.Vis;
using NDsl.Api.Core;
using NVisitor.Api.Lazy;

namespace NCase.Imp.Vis
{
    public class CaseGeneratorVisitors
        : ILazyVisitor<INode, ICaseGeneratorDirector, INode>
    {
        public IEnumerable<Pause> Visit(ICaseGeneratorDirector director, INode node)
        {
            if (node.Children.Any())
            {
                using (director.Push(node))
                {
                    foreach (INode child in node.Children)
                        foreach (Pause pause in director.Visit(child))
                            yield return pause;
                }
            }
            else
            {
                using (director.Push(node))
                {
                    // it's a leaf, so it is also a case: 
                    // then give hand to calling foreach in order to process the case
                    yield return Pause.Now; 
                }
            }

        }

    }
}
