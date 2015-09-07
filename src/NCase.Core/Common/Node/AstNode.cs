using System.Collections.Generic;
using NCase.Common.Quality;
using NCase.Core;

namespace NCase.Api.Dev
{
    public class AstNode : INode
    {
        private readonly List<RootNode> mRoots;

        public AstNode()
        {
            mRoots = new List<RootNode> {new RootNode()};
        }

        public bool IsReplaying { get; set; }
        /// <summary>NotNull and Contains at least one element</summary>
        [NotNull]
        public List<RootNode> Roots { get { return mRoots; } }
    }
}