using System.Linq;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.Combinations;
using NCaseFramework.Back.Api.Parse;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Ex;
using NDsl.Back.Api.Util;

namespace NCaseFramework.Back.Imp.Combinations
{
    public class AddChildVisitors
        : IAddChildVisitor<IProdNode, INode>
    {
        private const int INDENTATION_SIZE = 4;
        [NotNull] private readonly IFileCache mFileCache;
        [NotNull] private readonly ICodeLocationPrinter mCodeLocationPrinter;

        public AddChildVisitors([NotNull] IFileCache fileCache, [NotNull] ICodeLocationPrinter codeLocationPrinter)
        {
            mFileCache = fileCache;
            mCodeLocationPrinter = codeLocationPrinter;
        }

        public void Visit([NotNull] IAddChildDirector dir, [NotNull] IProdNode root, [NotNull] INode nodeToAdd)
        {
            // GRAMMAR:
            // prod   := union (emptyline union)*
            // union  := branch (newline branch)*  
            // branch := decl (INDENT prod DEDENT)?
            // decl   := (contribCall|ref...)

            CodeLocation codeLocation = nodeToAdd.CodeLocation;

            if (!root.Children.Any())
            {
                var unionNode = new UnionNode(new UnionId(), codeLocation);
                unionNode.AddBranch(new BranchNode(new BranchId(), codeLocation, nodeToAdd));
                root.AddUnion(unionNode);
            }

            int nodeToAddIndentation = GetIndentation(nodeToAdd);
            INode parent = FindBestParentRegardingToIndentation(root, nodeToAddIndentation, nodeToAdd);

            if (parent == null)
            {
                throw new InvalidSyntaxException(mCodeLocationPrinter,
                                                 codeLocation,
                                                 "No parent found to add the node corresponding to this statement");
            }

            INode previousSibling = parent.Children.LastOrDefault();

            if (parent is IProdNode)
            {
                
            }
            else if (parent is UnionNode)
            {
                
            }
            else if (parent is BranchNode)
            {
                
            }
            if (previousSibling == null)
            {
                parent.AddTreeBranch(new ProdNode(nodeToAdd.CodeLocation, new ProdId(), nodeToAdd));
                return;
            }

            bool isNewBranch = ExistBracketsBetweenSiblings(previousSibling, nodeToAdd);
            if (isNewBranch)
            {
                parent.AddTreeBranch(new ProdNode(nodeToAdd.CodeLocation, new ProdId(), nodeToAdd));
                return;
            }

            bool isNewSetOfCartesianProduct = ExistEmptyLineBetweenSiblings(previousSibling, nodeToAdd);
            if (isNewSetOfCartesianProduct)
            {
            }

            // else belongs to same set
        }

        [CanBeNull]
        private INode FindBestParentRegardingToIndentation([NotNull] INode parentCandidate,
                                                           int nodeToAddIndentation,
                                                           [NotNull] INode nodeToAdd)
        {
            INode lastChild = parentCandidate.Children.LastOrDefault();
            if (lastChild == null)
            {
                int parentCandidateIndentation = GetIndentation(parentCandidate);
                if (nodeToAddIndentation < parentCandidateIndentation + INDENTATION_SIZE)
                    throw new IndentationException(mCodeLocationPrinter, nodeToAdd.CodeLocation, "Invalid Indentation");

                return parentCandidate;
            }

            int lastChildIndentation = GetIndentation(lastChild);

            if (nodeToAddIndentation < lastChildIndentation)
                throw new IndentationException(mCodeLocationPrinter, nodeToAdd.CodeLocation, "Invalid Indentation");

            if (nodeToAddIndentation == lastChildIndentation)
                return parentCandidate;

            return FindBestParentRegardingToIndentation(lastChild, nodeToAddIndentation, nodeToAdd);
        }

        private bool ExistBracketsBetweenSiblings([NotNull] INode previousSibling, [NotNull] INode nodeToAdd)
        {
            int previousSiblingLineIndex;
            int nodeToAddLineIndex;
            string fileName;

            GetLineIndexes(
                           previousSibling,
                           nodeToAdd,
                           out previousSiblingLineIndex,
                           out nodeToAddLineIndex,
                           out fileName);

            for (int i = nodeToAddLineIndex; i >= previousSiblingLineIndex; i--)
            {
            }
            return false;
        }

        private bool ExistEmptyLineBetweenSiblings([NotNull] INode previousSibling, [NotNull] INode nodeToAdd)
        {
            int previousSiblingLineIndex;
            int nodeToAddLineIndex;
            string fileName;

            GetLineIndexes(
                           previousSibling,
                           nodeToAdd,
                           out previousSiblingLineIndex,
                           out nodeToAddLineIndex,
                           out fileName);

            for (int i = nodeToAddLineIndex; i >= previousSiblingLineIndex; i--)
            {
            }
            return false;
        }

        private void GetLineIndexes(
            [NotNull] INode node1,
            [NotNull] INode node2,
            out int node1LineIndex,
            out int node2LineIndex,
            out string fileName)
        {
            string node1FileName = GetFileName(node1.CodeLocation);
            string node2FileName = GetFileName(node2.CodeLocation);

            if (node1FileName != node2FileName)
            {
                throw new InvalidSyntaxException(mCodeLocationPrinter,
                                                 node2.CodeLocation,
                                                 "Both statements must be in the same file:\n- {0}\n-{1}",
                                                 mCodeLocationPrinter.Print(node1.CodeLocation),
                                                 mCodeLocationPrinter.Print(node2.CodeLocation));
            }

            node1LineIndex = GetLineIndex(node1.CodeLocation);
            node2LineIndex = GetLineIndex(node2.CodeLocation);
            fileName = node1FileName;
        }

        private int GetIndentation([NotNull] INode node)
        {
            string fileName = GetFileName(node.CodeLocation);
            int lineIndex = GetLineIndex(node.CodeLocation);
            return mFileCache.GetIndentation(fileName, lineIndex);
        }

        #region static helpers to move

        private static int GetLineIndex([NotNull] CodeLocation codeLocation)
        {
            int? lineIndex = codeLocation.Line;

            if (!lineIndex.HasValue)
                throw new CompilationException("lineIndex is not available: PDB file is required");

            return lineIndex.Value;
        }

        private static string GetFileName([NotNull] CodeLocation codeLocation)
        {
            string fileName = codeLocation.FileName;

            if (string.IsNullOrEmpty(fileName))
                throw new CompilationException("fileName not available: PDB file is required");

            return fileName;
        }

        #endregion
    }
}