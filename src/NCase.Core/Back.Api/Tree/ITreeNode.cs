using System.Collections.Generic;
using JetBrains.Annotations;
using NDsl.Back.Api;
using NDsl.Back.Api.Core;

namespace NCase.Back.Api.Tree
{
    public interface ITreeNode : IDefNode
    {
        [CanBeNull] INode Fact { get; }

        IEnumerable<INode> Branches { get; }
        TreeId Id { get; }
        void AddTreeBranch(INode branch);
    }
}