using JetBrains.Annotations;
using NCaseFramework.Back.Api.Parse;
using NCaseFramework.Back.Api.Seq;
using NDsl.Back.Api.Common;

namespace NCaseFramework.Back.Imp.Seq
{
    public class AddChildVisitors
        : IAddChildVisitor<ISeqNode, INode>
    {
        public void Visit([NotNull] IAddChildDirector dir, [NotNull] ISeqNode parent, [NotNull] INode child)
        {
            parent.AddChild(child);
        }
    }
}