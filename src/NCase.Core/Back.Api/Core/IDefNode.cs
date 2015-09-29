using JetBrains.Annotations;
using NDsl.Back.Api.Core;

namespace NCase.Back.Api.Core
{
    /// <summary>Case Set Definition Node </summary>
    public interface IDefNode : INode
    {
        [CanBeNull] IDefId DefId { get; }
    }
}