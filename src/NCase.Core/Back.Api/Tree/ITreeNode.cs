using System.Collections.Generic;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.SetDef;
using NDsl.Back.Api.Common;

namespace NCaseFramework.Back.Api.Tree
{
    public interface ITreeNode : ISetDefNode
    {
        [NotNull] new TreeId Id { get; }

        [CanBeNull] INode TreeFact { get; }

        [NotNull, ItemNotNull] IEnumerable<INode> Branches { get; }
        void AddTreeBranch([NotNull] INode branch);
    }
}