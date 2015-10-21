using System.Collections.Generic;
using NCaseFramework.Back.Api.Parse;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Def;
using NDsl.Back.Api.Ref;

namespace NCaseFramework.Back.Imp.Parse
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