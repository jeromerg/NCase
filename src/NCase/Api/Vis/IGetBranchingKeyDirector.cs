using NDsl.Api.Core.Nod;
using NVisitor.Api.Batch;

namespace NCase.Api.Vis
{
    public interface IGetBranchingKeyDirector : IDirector<INode, IGetBranchingKeyDirector>
    {
        /// <summary>
        /// if two nodes have the same branching key, then they will be put as children of
        /// the same parent node, resulting in a branching (new case).
        /// If BranchingKey is set to null then the node is an end node, that can not contribute to branching the tree
        /// </summary>
        object BranchingKey { get; set; }
    }
}