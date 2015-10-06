using JetBrains.Annotations;
using NDsl.Back.Api.Core;

namespace NCase.Back.Api.Prod
{
    /// <summary>
    ///     A child corresponds to a set of cases, which will be used in the cartesian product
    /// </summary>
    public interface IProdNode : IDefNode
    {
        [NotNull] ProdId Id { get; }
        void AddChild(INode child);
    }
}