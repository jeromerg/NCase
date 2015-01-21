using System.Collections.Generic;
using NCase.Api.Dev;

namespace NCase.Core
{
    public class RootNode : INode
    {
        private readonly List<INode> mChildren = new List<INode>();

        public List<INode> Children { get { return mChildren; } } 
    }
}