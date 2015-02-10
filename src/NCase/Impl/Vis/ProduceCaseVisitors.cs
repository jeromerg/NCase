using System;
using System.Collections.Generic;
using NCase.Api;
using NCase.Api.Vis;
using NDsl.Api.Core;
using NDsl.Impl.RecPlay;
using NVisitor.Api.Lazy;

namespace NCase.Impl.Vis
{
    public class ProduceCaseVisitors
        : ILazyVisitor<INode, IProduceCaseDir, IAstRoot>
        , ILazyVisitor<INode, IProduceCaseDir, ICaseSetNode>
        , ILazyVisitor<INode, IProduceCaseDir, INode>
        , ILazyVisitor<INode, IProduceCaseDir, RecPlayInterfacePropertyNode>
    {
        public IEnumerable<Pause> Visit(IProduceCaseDir director, IAstRoot node)
        {
            return PushNodeVisitChildrenandPopNode(director, node.Children, node);
        }

        public IEnumerable<Pause> Visit(IProduceCaseDir director, ICaseSetNode node)
        {
            return PushNodeVisitChildrenandPopNode(director, node.Children, node);
        }

        public IEnumerable<Pause> Visit(IProduceCaseDir director, INode node)
        {
            // TODO
            throw new NotImplementedException();
        }

        public IEnumerable<Pause> Visit(IProduceCaseDir director, RecPlayInterfacePropertyNode node)
        {
            director.CurrentCase.Push(node);
            yield return Pause.Now;
        }

        private static IEnumerable<Pause> PushNodeVisitChildrenandPopNode(IProduceCaseDir director, IEnumerable<INode> children, INode node)
        {
            director.CurrentCase.Push(node);
            try
            {
                foreach (INode child in children)
                    foreach (Pause pause in director.Visit(child))
                        yield return pause;
            }
            finally
            {
                director.CurrentCase.Pop();
            }
        }
    }
}
