using System.Collections.Generic;
using NCase.Back.Api.Parse;
using NDsl.Back.Api.Core;
using NDsl.Back.Api.Ref;

namespace NCase.Back.Imp.Parse
{
    public class GenerateCasesVisitors
        : IGenerateCaseVisitor<IRefNode<IDefNode>>
    {
        public IEnumerable<List<INode>> Visit(IGenerateCasesDirector dir, IRefNode<IDefNode> node, GenerateOptions options)
        {
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