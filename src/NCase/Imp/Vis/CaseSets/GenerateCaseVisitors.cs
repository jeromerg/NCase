using System;
using System.Collections.Generic;
using System.Linq;
using NCase.Api.Vis;
using NCase.Imp.Nod;
using NDsl.Api.Core.Nod;
using NVisitor.Api.Lazy;

namespace NCase.Imp.Vis.CaseSets
{
    public class GenerateCaseVisitors
        : ILazyVisitor<INode, IGenerateCaseDirector, ICaseTreeNode>
        , ILazyVisitor<INode, IGenerateCaseDirector, IRefNode<ICaseTreeNode>>
    {
        public IEnumerable<Pause> Visit(IGenerateCaseDirector director, ICaseTreeNode node)
        {
            IDisposable popHandle = null;
            if (node.Fact != null) 
                popHandle = director.Push(node.Fact);

            try
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
            finally
            {
                if(popHandle != null)
                    popHandle.Dispose();
            }
        }

        public IEnumerable<Pause> Visit(IGenerateCaseDirector director, IRefNode<ICaseTreeNode> node)
        {
            foreach(Pause pause in director.Visit(node.Reference))
                yield return Pause.Now;
        }

    }
}
