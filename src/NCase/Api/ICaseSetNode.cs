using System.Collections.Generic;
using NDsl.Api.Core;

namespace NCase.Api
{
    public interface ICaseSetNode : INode
    {
        IList<INode> Children { get; }
    }
}