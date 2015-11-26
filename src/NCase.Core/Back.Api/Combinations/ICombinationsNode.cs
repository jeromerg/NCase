using System.Collections.Generic;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.SetDef;
using NDsl.Back.Api.Common;

namespace NCaseFramework.Back.Api.Combinations
{
    public interface ICombinationsNode : ISetDefNode
    {
        [NotNull]
        new CombinationsId Id { get; }

        [CanBeNull] INode CasesOfThisTreeNode { get; }

        [NotNull, ItemNotNull] IEnumerable<ICombinationsNode> Branches { get; }

        void AddTreeBranch([NotNull] ICombinationsNode branch);
    }
}