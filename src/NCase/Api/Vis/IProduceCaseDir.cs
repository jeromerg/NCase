using System.Collections.Generic;
using NDsl.Api.Core;
using NVisitor.Api.Lazy;

namespace NCase.Api.Vis
{
    public interface IProduceCaseDir : ILazyDirector<INode, IProduceCaseDir>
    {
        Stack<INode> CurrentCase { get; }
    }
}