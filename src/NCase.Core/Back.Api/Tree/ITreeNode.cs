using System.Collections.Generic;
using JetBrains.Annotations;
using NCase.Back.Api.SetDef;
using NDsl.Back.Api.Common;

namespace NCase.Back.Api.Tree
{
    public interface ITreeNode : ISetDefNode
    {
        [NotNull] new TreeId Id { get; }

        [CanBeNull] INode Fact { get; }

        IEnumerable<INode> Branches { get; }
        void AddTreeBranch(INode branch);
    }
}