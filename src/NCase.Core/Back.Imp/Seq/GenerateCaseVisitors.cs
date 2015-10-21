using System.Collections.Generic;
using NCaseFramework.Back.Api.Parse;
using NCaseFramework.Back.Api.Seq;
using NDsl.Back.Api.Common;

namespace NCaseFramework.Back.Imp.Seq
{
    public class GenerateCaseVisitors
        : IGenerateCaseVisitor<ISeqNode>
    {
        public IEnumerable<List<INode>> Visit(IGenerateCasesDirector dir, ISeqNode node, GenerateOptions options)
        {
            foreach (List<INode> nodes in dir.Visit(node, options))
                yield return nodes;
        }
    }
}