using System.Linq;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.Combinations;
using NCaseFramework.Back.Api.Parse;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.RecPlay;

namespace NCaseFramework.Back.Imp.Combinations
{
    public class AddChildVisitors
        : IAddChildVisitor<ICombinationsNode, INode>,
          IAddChildVisitor<ICombinationsNode, IInterfaceRecPlayNode>
    {
        public void Visit([NotNull] IAddChildDirector dir, [NotNull] ICombinationsNode parent, [NotNull] IInterfaceRecPlayNode child)
        {
            var nodeToAdd = new CombinationsNode(child.CodeLocation, new CombinationsId(), child);

            ICombinationsNode nextBranch = parent.Branches.LastOrDefault();

            // no branch => end of recursion: this is the place
            if (nextBranch == null)
            {
                parent.AddTreeBranch(nodeToAdd);
                return;
            }

            var nextInterfaceRecPlayNode = nextBranch.CasesOfThisTreeNode as IInterfaceRecPlayNode;
            if (nextInterfaceRecPlayNode != null
                && Equals(nextInterfaceRecPlayNode.PropertyCallKey, child.PropertyCallKey))
            {
                // same key, then fork here
                parent.AddTreeBranch(nodeToAdd);
                return;
            }

            // ok keys are different, recurse
            dir.Visit(nextBranch, child);
        }

        public void Visit([NotNull] IAddChildDirector dir, [NotNull] ICombinationsNode parent, [NotNull] INode child)
        {
            var nodeToAdd = new CombinationsNode(child.CodeLocation, new CombinationsId(), child);

            INode nextBranch = parent.Branches.LastOrDefault();

            if (nextBranch == null)
                parent.AddTreeBranch(nodeToAdd);
            else
                dir.Visit(nextBranch, child);
        }
    }
}