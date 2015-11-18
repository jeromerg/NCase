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
                IEnumerable<List<INode>> casesOfRef = dir.Visit(node.Reference, options);

                if(casesOfRef == null)
                    throw new InvalidOperationException(string.Format("Visit of node {0} returned null", node.Reference));

                foreach (List<INode> caseFacts in casesOfRef)
                    yield return caseFacts;
            }
            else
            {
                yield return new List<INode> {node};
            }
        }
    }
}