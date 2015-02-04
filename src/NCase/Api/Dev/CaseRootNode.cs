using System.Collections.Generic;
using NDsl.Api.Dev;

namespace NCase.Api.Dev
{
    public class CaseRootNode : INode
    {
        private readonly List<INode> mChildren = new List<INode>();

        public List<INode> Children { get { return mChildren; } } 
    }
}