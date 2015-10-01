using System.Collections.Generic;
using NCase.Back.Api.Parse;
using NCase.Back.Api.Seq;
using NDsl.Back.Api.Core;
using NDsl.Back.Api.Ref;

namespace NCase.Back.Imp.Seq
{
    public class GenerateCaseVisitors
        : IGenerateCaseVisitor<ISeqNode>,
          IGenerateCaseVisitor<IRefNode<ISeqNode>>
    {
        public IEnumerable<List<INode>> Visit(IGenerateDirector director, IRefNode<ISeqNode> node)
        {
            return director.Visit(node.Reference);
        }

        public IEnumerable<List<INode>> Visit(IGenerateDirector director, ISeqNode node)
        {
            foreach (List<INode> nodes in director.Visit(node))
                yield return nodes;
        }
    }
}