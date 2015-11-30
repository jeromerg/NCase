using System;
using System.Configuration;
using System.Diagnostics;
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
        : IAddChildVisitor<ICombinationsNode, INode>
    {
        private const int INDENTATION_SIZE = 4;
        [NotNull] private readonly IFileCache mFileCache;

        public AddChildVisitors([NotNull] IFileCache fileCache)
        {
            mFileCache = fileCache;
        }

        public void Visit([NotNull] IAddChildDirector dir, [NotNull] ICombinationsNode root, [NotNull] INode nodeToAdd)
        {
            // grammar:
            // prod := (set (emptyline set)*)?;
            // set := (decl branch*)+
            // decl := (contribCall|ref...)
            // branch := { prod }

            // simplification in tree-model:
            // prod replaced by nested sets:
            //
            // (set1 emptyline set2 emptyline set3...) 
            // ==>
            // set(set1, set(set2, set(set3, null)
            //
            // As a result a set with a declaration equal to a set is a product (as defined above: may have only one set)

            int nodeToAddIndentation = GetIndentation(nodeToAdd);
            ICombinationsNode parent = FindBestParentRegardingToIndentation(root, nodeToAddIndentation, nodeToAdd);

            if(parent == null)
                throw new InvalidSyntaxException(nodeToAdd.CodeLocation, "No parent found to add the node corresponding to this statement");
            
            INode previousSibling = parent.Children.LastOrDefault();

            if(previousSibling == null)
            {
                parent.AddTreeBranch(new CombinationsNode(nodeToAdd.CodeLocation, new CombinationsId(), nodeToAdd));
                return;
            }

            bool isNewBranch = ExistBracketsBetweenSiblings(previousSibling, nodeToAdd);
            if(isNewBranch)
            {
                parent.AddTreeBranch(new CombinationsNode(nodeToAdd.CodeLocation, new CombinationsId(), nodeToAdd));
                return;
            }

            bool isNewSetOfCartesianProduct = ExistEmptyLineBetweenSiblings(previousSibling, nodeToAdd);
            if (isNewSetOfCartesianProduct)
            {
                
            }

            // else belongs to same set

        }

        [CanBeNull]
        private ICombinationsNode FindBestParentRegardingToIndentation(
            [NotNull] ICombinationsNode parentCandidate, 
            int nodeToAddIndentation, 
            [NotNull] INode nodeToAdd)
        {
            INode lastChild = parentCandidate.Children.LastOrDefault();
            if (lastChild == null)
            {
                int parentCandidateIndentation = GetIndentation(parentCandidate);
                if (nodeToAddIndentation < parentCandidateIndentation + INDENTATION_SIZE)
                    throw new IndentationException(nodeToAdd.CodeLocation, "Invalid Indentation");

                return parentCandidate; 
            }

            var lastChildAsCombinationsNode = lastChild as ICombinationsNode;
            if(lastChildAsCombinationsNode == null)
            {
                int lastChildIndentation = GetIndentation(lastChild);                
                if (nodeToAddIndentation != lastChildIndentation)
                    throw new IndentationException(nodeToAdd.CodeLocation, "Invalid Indentation");
                
                return parentCandidate;
            }

            ICombinationsNode betterParent = FindBestParentRegardingToIndentation(
                                                    lastChildAsCombinationsNode, 
                                                    nodeToAddIndentation, 
                                                    nodeToAdd);

            if (betterParent != null) 
                return betterParent;

            return parentCandidate;
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
                throw new InvalidSyntaxException(node2.CodeLocation,
                                                 "Both statements must be in the same file:\n- {0}\n-{1}", 
                                                 node1.CodeLocation.GetFullInfoWithSameSyntaxAsStackTrace(),
                                                 node2.CodeLocation.GetFullInfoWithSameSyntaxAsStackTrace());
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