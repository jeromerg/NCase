using System;
using System.Collections.Generic;
using NDsl.Api.Core.Nod;
using NVisitor.Api.Lazy;

namespace NCase.Api.Dev
{
    public interface IGenerateCaseDirector : ILazyDirector<INode, IGenerateCaseDirector>
    {
        // TODO MAKE Type Safer Push: accept only specific contract of nodes that can replay!
        IDisposable Push(INode node);

        // TODO MAKE Type Safer Push: accept only specific contract of nodes that can replay!
        IEnumerable<INode> CurrentCase { get; }
    }
}