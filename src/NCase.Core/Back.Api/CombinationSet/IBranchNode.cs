using JetBrains.Annotations;
using NCaseFramework.Back.Api.SetDef;
using NDsl.Back.Api.Common;

namespace NCaseFramework.Back.Api.CombinationSet
{
    public interface IBranchNode : ISetDefNode
    {
        new BranchId Id { get; }

        [NotNull] INode Declaration { get; }

        [CanBeNull] IProdNode Product { get; set;}
    }
}