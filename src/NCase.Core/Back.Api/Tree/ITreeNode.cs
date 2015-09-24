using System.Collections.Generic;
using JetBrains.Annotations;
using NCase.Back.Api.Core;
using NDsl.Api.Core;


namespace NCase.Back.Api.Tree
{
    public interface ITreeNode : IDefNode
    {
        [CanBeNull] INode Fact { get; }

        IEnumerable<INode> Branches { get; }
        void AddTreeBranch(INode branch);
    }
}