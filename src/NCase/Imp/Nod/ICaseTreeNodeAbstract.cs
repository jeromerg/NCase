using System.Collections.Generic;
using NDsl.Api.Core.Nod;

namespace NCase.Imp.Nod
{
    public interface ICaseTreeNodeAbstract : INode
    {
        IEnumerable<ICaseTreeNodeAbstract> Branches { get; }
        void PlaceNextNode(INode child);
    }
}