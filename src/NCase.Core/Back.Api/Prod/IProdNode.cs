using NDsl.Back.Api;
using NDsl.Back.Api.Core;

namespace NCase.Back.Api.Prod
{
    /// <summary>
    ///     A child corresponds to a set of cases, which will be used in the cartesian product
    /// </summary>
    public interface IProdNode : IDefNode
    {
        ProdId Id { get; }
        void AddChild(INode child);
    }
}