using System.Collections.Generic;
using JetBrains.Annotations;
using NDsl.Back.Api;
using NDsl.Back.Api.Core;

namespace NCase.Back.Api.Tree
{
    public interface ITreeNode : IDefNode
    {
        [NotNull] TreeId Id { get; }

        [CanBeNull] INode Fact { get; }

        IEnumerable<INode> Branches { get; }
        void AddTreeBranch(INode branch);
    }
}