using System.Collections.Generic;
using NDsl.Api.Core.Nod;

namespace NCase.Api.Nod
{
    public interface ICaseBranchNode : INode
    {
        INode Fact { get; }
        IEnumerable<ICaseBranchNode> SubBranches { get; }
    }
}