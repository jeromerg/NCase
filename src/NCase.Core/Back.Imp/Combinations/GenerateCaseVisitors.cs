﻿using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.Combinations;
using NCaseFramework.Back.Api.Parse;
using NCaseFramework.Back.Api.Util;
using NDsl.Back.Api.Common;

namespace NCaseFramework.Back.Imp.Combinations
{
    public class GenerateCaseVisitors
        : IGenerateCaseVisitor<ICombinationsNode>
    {
        [NotNull, ItemNotNull]
        public IEnumerable<List<INode>> Visit([NotNull] IGenerateCasesDirector dir,
                                              [NotNull] ICombinationsNode node,
                                              [NotNull] GenerateOptions options)
        {
            if (options == null) throw new ArgumentNullException("options");

            if (node.CasesOfThisTreeNode != null)
            {
                // IF THE TREE NODE HAS A TREE FACT; THEN COMBINE ITS CASES WITH THE CASES GENERATED BY THE BRANCHES

                IEnumerable<List<INode>> casesOfThisTreeNode = dir.Visit(node.CasesOfThisTreeNode, options);

                if (casesOfThisTreeNode == null)
                    throw new InvalidOperationException(string.Format("Visit of child {0} returned null", node.CasesOfThisTreeNode));

                foreach (List<INode> caseOfThisTreeNode in casesOfThisTreeNode)
                {
                    if (caseOfThisTreeNode == null)
                    {
                        string msg = string.Format("Visit of child {0} returned a case having the list of facts equal to null",
                                                       node.CasesOfThisTreeNode);
                        throw new InvalidOperationException(msg);
                    }

                    foreach (List<INode> caseOfBranches in VisitTreeBranches(dir, node, options))
                        yield return ListUtil.Concat(caseOfThisTreeNode, caseOfBranches);
                }
            }
            else
            {
                // IF THE TREE NODE DOESN'T HAVE ANY FACT, THEN FORWARD THE CASES GENERATED BY THE BRANCHES
                foreach (List<INode> nodes in VisitTreeBranches(dir, node, options))
                    yield return nodes;
            }
        }

        [NotNull, ItemNotNull]
        private IEnumerable<List<INode>> VisitTreeBranches([NotNull] IGenerateCasesDirector dir,
                                                           [NotNull] ICombinationsNode node,
                                                           [NotNull] GenerateOptions options)
        {
            if (options == null) throw new ArgumentNullException("options");


            if (!node.Branches.Any())
            {
                // it's a leaf
                yield return new List<INode>();
            }
            else
            {
                foreach (ICombinationsNode branchNode in node.Branches)
                {
                    IEnumerable<List<INode>> casesOfBranch = dir.Visit(branchNode, options);

                    if (casesOfBranch == null)
                        throw new InvalidOperationException(string.Format("Visit of node {0} returned null", branchNode));

                    foreach (List<INode> nodes in casesOfBranch)
                        yield return nodes;
                }
            }
        }
    }
}