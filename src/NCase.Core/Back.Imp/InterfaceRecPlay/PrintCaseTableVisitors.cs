using JetBrains.Annotations;
using NCase.Back.Api.Print;
using NDsl.Back.Api.RecPlay;
using NDsl.Back.Api.Util.Table;

namespace NCase.Back.Imp.InterfaceRecPlay
{
    public class PrintCaseTableVisitors : IPrintCaseTableVisitor<IInterfaceRecPlayNode>
    {
        public void Visit(IPrintCaseTableDirector director, IInterfaceRecPlayNode node)
        {
            director.Print(node.CodeLocation,
                           new InterfaceRecPlayNodeColumn(node),
                           node.PropertyValue != null ? node.PropertyValue.ToString() : "null");
        }
    }

    public class InterfaceRecPlayNodeColumn : ITableColumn
    {
        [NotNull] private readonly IInterfaceRecPlayNode mNode;

        public InterfaceRecPlayNodeColumn(IInterfaceRecPlayNode node)
        {
            mNode = node;
        }

        public HorizontalAlignment HorizontalAlignment
        {
            get { return HorizontalAlignment.Right; }
        }

        public string Title
        {
            get { return mNode.PrintInvocation(); }
        }

        protected bool Equals(InterfaceRecPlayNodeColumn other)
        {
            return Equals(mNode.PropertyCallKey, other.mNode.PropertyCallKey);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((InterfaceRecPlayNodeColumn) obj);
        }

        public override int GetHashCode()
        {
            return mNode.PropertyCallKey.GetHashCode();
        }
    }
}