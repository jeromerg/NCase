using NCase.Back.Api.Parse;
using NCase.Back.Api.Seq;
using NDsl.Back.Api.Common;

namespace NCase.Back.Imp.Seq
{
    public class AddChildVisitors
        : IAddChildVisitor<ISeqNode, INode>
    {
        public void Visit(IAddChildDirector dir, ISeqNode parent, INode child)
        {
            parent.AddChild(child);
        }
    }
}