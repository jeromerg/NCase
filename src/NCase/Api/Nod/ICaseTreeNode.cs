using System.Collections.Generic;
using NDsl.Api.Core;

namespace NCase.Api
{
    public interface ICaseTreeNode : INode
    {
        IList<INode> Children { get; }
    }
}