using System.Collections.Generic;
using NVisitor.Common.Quality;

namespace NDsl.Api.Core
{
    public interface IAstRoot : INode
    {
        AstState State { get; set; }

        /// <summary>NotNull and Contains at least one element</summary>
        IEnumerable<INode> Children { get; }

        void AddChild([NotNull] INode child);
    }
}