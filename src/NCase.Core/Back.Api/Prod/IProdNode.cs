using JetBrains.Annotations;
using NCaseFramework.Back.Api.SetDef;
using NDsl.Back.Api.Common;

namespace NCaseFramework.Back.Api.Prod
{
    /// <summary>
    ///     A child corresponds to a set of cases, which will be used in the cartesian product
    /// </summary>
    public interface IProdNode : ISetDefNode
    {
        [NotNull] new ProdId Id { get; }
        void AddChild(INode child);
    }
}