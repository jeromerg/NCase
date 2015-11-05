using JetBrains.Annotations;
using NDsl.All.Def;
using NDsl.Back.Api.Common;

namespace NDsl.Back.Api.Def
{
    /// <summary>Case Set Definition Node </summary>
    public interface IDefNode : INode
    {
        [NotNull] IDefId Id { get; }
    }
}