using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.Parse;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Def;
using NDsl.Back.Api.Ref;

namespace NCaseFramework.Back.Imp.Parse
{
    public class GenerateCasesVisitors
        : IGenerateCaseVisitor<IRefNode<IDefNode>>
    {
        [NotNull, ItemNotNull]
        public IEnumerable<List<INode>> Visit([NotNull] IGenerateCasesDirector dir,
                                              [NotNull] IRefNode<IDefNode> node,
                                              [NotNull] GenerateOptions options)
        {
            if (options == null) throw new ArgumentNullException("options");

            if (options.IsRecursive)
            {
                foreach (List<INode> subCases in dir.Visit(node.Reference, options))
                    yield return subCases;
            }
            else
            {
                yield return new List<INode> {node};
            }
        }
    }
}