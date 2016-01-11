using System.Collections.Generic;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.SetDef;

namespace NCaseFramework.Back.Api.Combinations
{
    public interface IUnionNode : ISetDefNode
    {
        [NotNull] new UnionId Id { get; }
        [NotNull, ItemNotNull] IEnumerable<IBranchNode> Branches { get; }
        void AddBranch([NotNull] IBranchNode union);
    }
}