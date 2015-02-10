using System;
using System.Collections.Generic;
using NCase.Api;
using NCase.Api.Vis;
using NDsl.Api.Core;
using NDsl.Impl.RecPlay;
using NVisitor.Api.Lazy;

namespace NCase.Impl.Vis
{
    public class IterateCaseVisitors
        : ILazyVisitor<INode, IIterateCaseDirector, IAstRoot>
        , ILazyVisitor<INode, IIterateCaseDirector, ICaseSetNode>
        , ILazyVisitor<INode, IIterateCaseDirector, INode>
        , ILazyVisitor<INode, IIterateCaseDirector, RecPlayInterfacePropertyNode>
    {
        public IEnumerable<Pause> Visit(IIterateCaseDirector director, IAstRoot node)
        {
            return PushNodeVisitChildrenandPopNode(director, node.Children, node);
        }

        public IEnumerable<Pause> Visit(IIterateCaseDirector director, ICaseSetNode node)
        {
            return PushNodeVisitChildrenandPopNode(director, node.Children, node);
        }

        public IEnumerable<Pause> Visit(IIterateCaseDirector director, INode node)
        {
            // TODO
            throw new NotImplementedException();
        }

        public IEnumerable<Pause> Visit(IIterateCaseDirector director, RecPlayInterfacePropertyNode node)
        {
            director.CurrentCase.Push(node);
            yield return Pause.Now;
        }

        private static IEnumerable<Pause> PushNodeVisitChildrenandPopNode(IIterateCaseDirector director, IEnumerable<INode> children, INode node)
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
