using System.Collections.Generic;
using NDsl.Api.Core;
using NDsl.Api.Core.Nod;

namespace NCase.Api.Nod
{
    public interface ICaseBranchNode : IExtendableNode, ILocated
    {
        INode Fact { get; }
        IEnumerable<INode> SubBranches { get; }
    }
}