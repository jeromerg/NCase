using JetBrains.Annotations;

namespace NDsl.Back.Api.Core
{
    /// <summary>Case Set Definition Node </summary>
    public interface IDefNode : INode
    {
        [NotNull] IDefId DefId { get; }
    }
}