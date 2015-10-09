using System.Collections.Generic;
using JetBrains.Annotations;

namespace NDsl.Back.Api.Common
{
    /// <summary>Node contributing to the Normalized Heterogeneous AST</summary>
    public interface INode : ILocated
    {
        [NotNull, ItemNotNull] IEnumerable<INode> Children { get; }
    }
}