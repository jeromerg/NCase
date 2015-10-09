using System.Collections.Generic;
using NCase.Back.Api.Parse;
using NCase.Back.Api.Seq;
using NDsl.Back.Api.Common;

namespace NCase.Back.Imp.Seq
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