using System;
using System.Linq;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.CombinationSet;
using NCaseFramework.Back.Api.Parse;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Ex;
using NDsl.Back.Api.Util;

namespace NCaseFramework.Back.Imp.CombinationSet
{
    public class AddChildVisitors
        : IAddChildVisitor<ICombinationSetNode, INode>
    {
        private const int INDENTATION_SIZE = 4;
        [NotNull] private readonly IFileCache mFileCache;
        [NotNull] private readonly ICodeLocationPrinter mCodeLocationPrinter;

        public AddChildVisitors([NotNull] IFileCache fileCache, [NotNull] ICodeLocationPrinter codeLocationPrinter)
        {
            mFileCache = fileCache;
            mCodeLocationPrinter = codeLocationPrinter;
        }

        public void Visit([NotNull] IAddChildDirector dir, [NotNull] ICombinationSetNode combinationSetNode, [NotNull] INode nodeToAdd)
        {
            if (combinationSetNode.Product == null)
            {
                combinationSetNode.Product = CreateNewProductUnionBranchTriplet(nodeToAdd, combinationSetNode.IsOnlyPairwise);
                return;
            }
            
            AddChildRecursive(combinationSetNode.Product, nodeToAdd, combinationSetNode.IsOnlyPairwise);
        }

        private void AddChildRecursive([NotNull] IProdNode parentCandidate, [NotNull] INode nodeToAdd, bool isOnlyPairwise)
        {
            // GRAMMAR:
            // prod   := union (emptyline union)*
            // union  := branch (newline branch)*  
            // branch := decl (INDENT prod DEDENT)?
            // decl   := (contribCall|ref...)

            int currentProductIndentation = GetIndentation(parentCandidate);
            int nodeToAddIndentation = GetIndentation(nodeToAdd);

            IUnionNode lastUnion = parentCandidate.Unions.Last();
            if (lastUnion == null) throw new ArgumentException("lastUnion is null"); // remark: resharper false negative

            IBranchNode lastBranch = lastUnion.Branches.Last();
            if (lastBranch == null) throw new ArgumentException("lastBranch is null"); // remark: resharper false negative


            if (nodeToAddIndentation == currentProductIndentation)
            {
                bool isNewUnion = ExistEmptyLineBetweenSiblings(lastBranch, nodeToAdd);

                var newBranchNode = new BranchNode(new BranchId(), nodeToAdd.CodeLocation, nodeToAdd);

                if (isNewUnion)
                {
                    var newUnionNode = new UnionNode(new UnionId(), nodeToAdd.CodeLocation);
                    newUnionNode.AddBranch(newBranchNode);
                    parentCandidate.AddUnion(newUnionNode);
                }
                else
                {
                    // ReSharper disable once PossibleNullReferenceException
                    parentCandidate.Unions.Last().AddBranch(newBranchNode);
                }
            }
            else if (nodeToAddIndentation >= currentProductIndentation + INDENTATION_SIZE)
            {
                if(lastBranch.Product == null)
                {
                    ProdNode newProdNode = CreateNewProductUnionBranchTriplet(nodeToAdd, isOnlyPairwise);
                    lastBranch.Product = newProdNode;
                }
                else
                {
                    AddChildRecursive(lastBranch.Product, nodeToAdd, isOnlyPairwise); // recursion
                }
            }
            else 
            {
                throw new IndentationException(mCodeLocationPrinter, nodeToAdd.CodeLocation, "Invalid Indentation");
            }
            
        }

        private static ProdNode CreateNewProductUnionBranchTriplet([NotNull] INode nodeToAdd, bool isOnlyPairwise)
        {
            var newBranchNode = new BranchNode(new BranchId(), nodeToAdd.CodeLocation, nodeToAdd);
            var newUnionNode = new UnionNode(new UnionId(), nodeToAdd.CodeLocation);

            ProdNode newProdNode;
            {

                if (isOnlyPairwise) 
                    newProdNode = new PairwiseProdNode(new PairwiseProdId(), nodeToAdd.CodeLocation);
                else 
                    newProdNode = new CartesianProdNode(new CartesianProdId(), nodeToAdd.CodeLocation);
            }

            newUnionNode.AddBranch(newBranchNode);
            newProdNode.AddUnion(newUnionNode);
            return newProdNode;
        }

        private bool ExistEmptyLineBetweenSiblings([NotNull] INode previousSibling, [NotNull] INode nodeToAdd)
        {
            string previousSiblingFileName = GetFileName(previousSibling.CodeLocation);
            string fileName = GetFileName(nodeToAdd.CodeLocation);

            if (previousSiblingFileName != fileName)
            {
                throw new InvalidSyntaxException(mCodeLocationPrinter,
                                                 nodeToAdd.CodeLocation,
                                                 "Both statements must be in the same file:\n- {0}\n-{1}",
                                                 mCodeLocationPrinter.Print(previousSibling.CodeLocation),
                                                 mCodeLocationPrinter.Print(nodeToAdd.CodeLocation));
            }

            int previousSiblingLineIndex = GetLineIndex(previousSibling.CodeLocation);
            int nodeToAddLineIndex = GetLineIndex(nodeToAdd.CodeLocation);

            for (int lineIndex = nodeToAddLineIndex-1; lineIndex > previousSiblingLineIndex; lineIndex--)
            {
                string line = mFileCache.GetLine(fileName, lineIndex);
                bool isEmptyLine = string.IsNullOrWhiteSpace(line);
                if(isEmptyLine)
                    return true;
            }
            return false;
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