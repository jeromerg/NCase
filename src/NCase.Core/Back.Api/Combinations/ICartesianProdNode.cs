using JetBrains.Annotations;
using NCaseFramework.Back.Api.SetDef;

namespace NCaseFramework.Back.Api.Combinations
{
    public interface ICartesianProdNode : ISetDefNode
    {
        [NotNull] new CartesianProdId Id { get; }
    }
}