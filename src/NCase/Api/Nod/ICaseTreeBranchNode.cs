using System.Collections.Generic;
using NCase.Imp.Nod;
using NDsl.Api.Core.Nod;

namespace NCase.Api.Nod
{
    public interface ICaseTreeBranchNode : INode, ICaseTreeNodeAbstract
    {
        INode Fact { get; }
    }
}