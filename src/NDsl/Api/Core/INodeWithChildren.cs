using System.Collections.Generic;
using NVisitor.Common.Quality;

namespace NDsl.Api.Core
{
    /// <summary>Interface that can be used to implement homogeneous AST or normalized heterogeneous AST</summary>
    public interface INodeWithChildren : INode
    {
        [NotNull] IEnumerable<INode> Children { get; }
    }
}