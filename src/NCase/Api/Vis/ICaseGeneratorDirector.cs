using System;
using System.Collections.Generic;
using NDsl.Api.Core;
using NVisitor.Api.Lazy;

namespace NCase.Api.Vis
{
    public interface ICaseGeneratorDirector : ILazyDirector<INode, ICaseGeneratorDirector>
    {
        IDisposable Push(INode node);

        IEnumerable<INode> CurrentCase { get; }
    }
}