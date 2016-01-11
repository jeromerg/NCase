using System.Collections.Generic;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.SetDef;

namespace NCaseFramework.Back.Api.CombinationSet
{
    public interface IProdNode : ISetDefNode
    {
        [NotNull] new ProdId Id { get; }

        [NotNull, ItemNotNull] IEnumerable<IUnionNode> Unions { get; }

        void AddUnion([NotNull] IUnionNode union);
    }
}