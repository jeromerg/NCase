using System.Collections.Generic;
using NDsl.Api.Core;
using NVisitor.Common.Quality;

namespace NDsl.Impl.Core
{
    public class AstRoot : IAstRoot
    {
        private readonly List<INode> mChildren;

        public AstRoot()
        {
            mChildren = new List<INode>();
        }

        public AstState State { get; set; }

        [NotNull]
        public IEnumerable<INode> Children { get { return mChildren; } }

        public void AddChild(INode child)
        {
            mChildren.Add(child);
        }
    }
}