using System.Collections.Generic;
using NDsl.Api.Core;
using NVisitor.Api.Lazy;

namespace NCase.Api.Vis
{
    public interface IIterateCaseDirector : ILazyDirector<INode, IIterateCaseDirector>
    {
        Stack<INode> CurrentCase { get; }
    }
}