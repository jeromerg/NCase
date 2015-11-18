using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.Parse;
using NCaseFramework.Back.Api.Seq;
using NDsl.Back.Api.Common;

namespace NCaseFramework.Back.Imp.Seq
{
    public class GenerateCaseVisitors
        : IGenerateCaseVisitor<ISeqNode>
    {
        [NotNull, ItemNotNull]
        public IEnumerable<List<INode>> Visit([NotNull] IGenerateCasesDirector dir,
                                              [NotNull] ISeqNode node,
                                              [NotNull] GenerateOptions options)
        {
            if (options == null) throw new ArgumentNullException("options");

            IEnumerable<List<INode>> factsForAllCases = dir.Visit(node, options);

            if (factsForAllCases == null)
                throw new InvalidOperationException(string.Format("Visit of node {0} returned null", node));

            foreach (List<INode> nodes in factsForAllCases)
                yield return nodes;
        }
    }
}