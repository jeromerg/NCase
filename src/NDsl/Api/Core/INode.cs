using System.Collections.Generic;
using NVisitor.Common.Quality;

namespace NDsl.Api.Core
{
    /// <summary>Node contributing to the Normalized Heterogeneous AST</summary>
    public interface INode : ILocated
    {
        [NotNull] IEnumerable<INode> Children { get; }
    }
}