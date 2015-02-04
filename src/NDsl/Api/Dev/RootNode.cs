using System.Collections.Generic;
using NCase.Util.Quality;

namespace NDsl.Api.Dev
{
    public class RootNode : INode
    {
        private readonly List<INode> mChildren;

        public RootNode()
        {
            mChildren = new List<INode>();
        }

        public bool IsReplaying { get; set; }

        /// <summary>NotNull and Contains at least one element</summary>
        [NotNull]
        public List<INode> Children { get { return mChildren; } }
    }
}