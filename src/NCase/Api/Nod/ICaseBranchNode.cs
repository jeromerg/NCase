using System.Collections.Generic;
using NDsl.Api.Core;

namespace NCase.Api.Nod
{
    public interface ICaseBranchNode : IExtendableNode, ILocated
    {
        INode CaseFact { get; }
        IEnumerable<INode> SubBranches { get; }
    }
}