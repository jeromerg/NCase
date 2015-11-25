using System.Collections.Generic;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.SetDef;
using NDsl.Back.Api.Common;

namespace NCaseFramework.Back.Api.Tree
{
    public interface ITreeNode : ISetDefNode
    {
        [NotNull] new TreeId Id { get; }

        [CanBeNull] INode CasesOfThisTreeNode { get; }

        [NotNull, ItemNotNull] IEnumerable<ITreeNode> Branches { get; }
        void AddTreeBranch([NotNull] ITreeNode branch);
    }
}