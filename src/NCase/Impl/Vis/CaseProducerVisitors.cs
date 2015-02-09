using System;
using System.Collections.Generic;
using System.Linq;
using NCase.Api;
using NCase.Api.Vis;
using NDsl.Api.Core;
using NVisitor.Api.Lazy;

namespace NCase.Impl.Vis
{
    public class CaseProducerVisitors
        : ILazyVisitor<INode, IProduceCaseDir, IAstRoot>
        , ILazyVisitor<INode, IProduceCaseDir, ICaseSetNode>
        , ILazyVisitor<INode, IProduceCaseDir, INode>
    {
        public IEnumerable<Pause> Visit(IProduceCaseDir director, IAstRoot node)
        {
            return node.Children.SelectMany(child => director.Visit(child));
        }

        public IEnumerable<Pause> Visit(IProduceCaseDir director, ICaseSetNode node)
        {
            return node.Children.SelectMany(child => director.Visit(child));
        }

        public IEnumerable<Pause> Visit(IProduceCaseDir director, INode node)
        {
            // TODO
            throw new NotImplementedException();
        }

    }
}
