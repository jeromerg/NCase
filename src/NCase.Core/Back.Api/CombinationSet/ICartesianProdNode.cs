using JetBrains.Annotations;
using NCaseFramework.Back.Api.SetDef;

namespace NCaseFramework.Back.Api.CombinationSet
{
    public interface ICartesianProdNode : ISetDefNode
    {
        [NotNull] new CartesianProdId Id { get; }
    }
}