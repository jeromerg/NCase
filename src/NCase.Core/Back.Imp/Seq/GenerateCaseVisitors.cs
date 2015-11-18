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

            foreach (List<INode> nodes in dir.Visit(node, options))
                yield return nodes;
        }
    }
}