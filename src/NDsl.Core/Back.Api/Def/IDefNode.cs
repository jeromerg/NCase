using JetBrains.Annotations;
using NDsl.Back.Api.Common;

namespace NDsl.Back.Api.Def
{
    /// <summary>Case Set Definition Node </summary>
    public interface IDefNode : INode
    {
        [NotNull] IDefId Id { get; }
    }
}