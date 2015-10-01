using JetBrains.Annotations;
using NDsl.Back.Api.Core;

namespace NDsl.Back.Api
{
    /// <summary>Case Set Definition Node </summary>
    public interface IDefNode : INode
    {
        [NotNull] IDefId DefId { get; }
    }
}