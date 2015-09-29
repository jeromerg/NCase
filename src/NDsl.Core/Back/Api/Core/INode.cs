using System.Collections.Generic;
using JetBrains.Annotations;

namespace NDsl.Back.Api.Core
{
    /// <summary>Node contributing to the Normalized Heterogeneous AST</summary>
    public interface INode : ILocated
    {
        [NotNull] IEnumerable<INode> Children { get; }
    }
}