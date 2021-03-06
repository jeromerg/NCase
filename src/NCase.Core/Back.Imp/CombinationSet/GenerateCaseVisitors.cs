﻿using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.CombinationSet;
using NCaseFramework.Back.Api.Parse;
using NCaseFramework.Back.Api.Util;
using NDsl.Back.Api.Common;
using NUtil.Math.Combinatorics.Pairwise;

namespace NCaseFramework.Back.Imp.CombinationSet
{
    public class GenerateCaseVisitors
        : IGenerateCaseVisitor<ICombinationSetNode>,
          IGenerateCaseVisitor<IProdNode>,
          IGenerateCaseVisitor<IPairwiseProdNode>,
          IGenerateCaseVisitor<IUnionNode>,
          IGenerateCaseVisitor<IBranchNode>
    {
        [NotNull] private readonly IPairwiseGenerator mPairwiseGenerator;

        public GenerateCaseVisitors([NotNull] IPairwiseGenerator pairwiseGenerator)
        {
            mPairwiseGenerator = pairwiseGenerator;
        }

        public IEnumerable<List<INode>> Visit([NotNull] IGenerateCasesDirector dir,
                                              [NotNull] IBranchNode node,
                                              [NotNull] GenerateOptions options)
        {
            IEnumerable<List<INode>> declarationCasesFacts = dir.Visit(node.Declaration, options);

            if (declarationCasesFacts == null)
                throw new ArgumentException("declarationCasesFacts is null");


            if (node.Product == null)
            {
                // it is a LEAF 
                foreach (List<INode> declarationCaseFacts in declarationCasesFacts)
                {
                    if (declarationCaseFacts == null)
                        throw new InvalidOperationException("Visit returned a list of facts equal to null");

                    yield return declarationCaseFacts;
                }
            }
            else
            {
                // IT IS A BRANCH
                IEnumerable<List<INode>> subBranchCasesFacts = dir.Visit(node.Product, options);

                if (subBranchCasesFacts == null)
                    throw new InvalidOperationException("Visit returned a list of facts equal to null");

                // ReSharper disable PossibleMultipleEnumeration
                if (!declarationCasesFacts.Any())
                {
                    foreach (List<INode> subBranchCaseFacts in subBranchCasesFacts)
                    {
                        if (subBranchCaseFacts == null)
                            throw new InvalidOperationException("Visit returned a list containing an item equal to null");

                        yield return subBranchCaseFacts;
                    }
                }
                else
                {
                    foreach (List<INode> declarationCaseFacts in declarationCasesFacts)
                    {
                        if (declarationCaseFacts == null)
                            throw new InvalidOperationException("Visit returned a list of facts equal to null");

                        foreach (List<INode> subBranchCaseFacts in subBranchCasesFacts)
                        {
                            if (subBranchCaseFacts == null)
                                throw new InvalidOperationException("Visit returned a list containing an item equal to null");

                            yield return ListUtil.Concat(declarationCaseFacts, subBranchCaseFacts);
                        }
                    }
                }
                // ReSharper restore PossibleMultipleEnumeration
            }
        }

        public IEnumerable<List<INode>> Visit([NotNull] IGenerateCasesDirector dir,
                                              [NotNull] ICombinationSetNode node,
                                              [NotNull] GenerateOptions options)
        {
            if (node.Product == null)
                return Enumerable.Empty<List<INode>>();

            return dir.Visit(node.Product, options);
        }

        public IEnumerable<List<INode>> Visit([NotNull] IGenerateCasesDirector dir,
                                              [NotNull] IPairwiseProdNode node,
                                              [NotNull] GenerateOptions options)
        {
            if (options == null) throw new ArgumentNullException("options");

            // COLLECT ALL SUBCASES
            var allUnionsCasesFacts = new List<List<List<INode>>>();

            foreach (IUnionNode unionNode in node.Unions) // foreach dim
            {
                IEnumerable<List<INode>> unionCasesFacts = dir.Visit(unionNode, options);

                if (unionCasesFacts == null)
                    throw new InvalidOperationException(string.Format("Visit of child {0} returned null", unionNode));

                allUnionsCasesFacts.Add(unionCasesFacts.ToList());
            }

            // GENERATE AND YIELD ALL PAIRWISE TEST CASES
            {
                // ReSharper disable once PossibleNullReferenceException
                int[] dimSizes = allUnionsCasesFacts.Select(dimCases => dimCases.Count).ToArray();

                IEnumerable<int[]> pairwiseDimValueIndexes = mPairwiseGenerator.Generate(dimSizes);

                foreach (int[] valueForEachDim in pairwiseDimValueIndexes)
                {
                    var nodes = new List<INode>();
                    for (int dim = 0; dim < valueForEachDim.Length; dim++)
                    {
                        int value = valueForEachDim[dim];
                        // ReSharper disable once PossibleNullReferenceException
                        // ReSharper disable once AssignNullToNotNullAttribute
                        nodes.AddRange(allUnionsCasesFacts[dim][value]);
                    }
                    yield return nodes;
                }
            }
        }

        public IEnumerable<List<INode>> Visit([NotNull] IGenerateCasesDirector dir,
                                              [NotNull] IProdNode node,
                                              [NotNull] GenerateOptions options)
        {
            if (options == null) throw new ArgumentNullException("options");

            if (!node.Unions.Any())
                return Enumerable.Empty<List<INode>>();
            else
                return ProduceCartesianProductRecursively(dir, node.Unions.ToList(), 0, options);
        }

        public IEnumerable<List<INode>> Visit([NotNull] IGenerateCasesDirector dir,
                                              [NotNull] IUnionNode node,
                                              [NotNull] GenerateOptions options)
        {
            foreach (IBranchNode branch in node.Branches)
            {
                IEnumerable<List<INode>> branchCasesFacts = dir.Visit(branch, options);

                if (branchCasesFacts == null)
                    throw new ArgumentException("branchCasesFacts is null");

                foreach (List<INode> branchCaseFacts in branchCasesFacts)
                    yield return branchCaseFacts;
            }
        }

        [NotNull, ItemNotNull]
        private IEnumerable<List<INode>> ProduceCartesianProductRecursively([NotNull] IGenerateCasesDirector dir,
                                                                            [NotNull, ItemNotNull] List<IUnionNode> unions,
                                                                            int unionIndex,
                                                                            [NotNull] GenerateOptions options)
        {
            bool isLastUnion = unionIndex == unions.Count - 1;
            INode currentUnionNode = unions[unionIndex];

            IEnumerable<List<INode>> currentUnionCases = dir.Visit(currentUnionNode, options);

            if (currentUnionCases == null)
                throw new InvalidOperationException("Visit of child returned null");

            foreach (List<INode> caseFacts in currentUnionCases)
            {
                if (caseFacts == null)
                    throw new InvalidOperationException("Visit returned a case having the list of facts equal to null");

                if (isLastUnion)
                {
                    yield return caseFacts; // end of recursion here
                }
                else
                {
                    // continue recursion and merge facts together
                    IEnumerable<List<INode>> nextDims = ProduceCartesianProductRecursively(dir, unions, unionIndex + 1, options);

                    foreach (List<INode> subnodes in nextDims)
                        yield return ListUtil.Concat(caseFacts, subnodes);
                }
            }
        }
    }
}