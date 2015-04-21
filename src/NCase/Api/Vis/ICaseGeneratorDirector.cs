using System;
using System.Collections.Generic;
using NDsl.Api.Core;
using NVisitor.Api.Lazy;

namespace NCase.Api.Vis
{
    public interface ICaseGeneratorDirector : ILazyDirector<INode, ICaseGeneratorDirector>
    {
        // TODO MAKE Type Safer Push: accept only specific contract of nodes that can replay!
        IDisposable Push(INode node);

        // TODO MAKE Type Safer Push: accept only specific contract of nodes that can replay!
        IEnumerable<INode> CurrentCase { get; }
    }
}