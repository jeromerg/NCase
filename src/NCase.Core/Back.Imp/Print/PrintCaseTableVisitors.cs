using JetBrains.Annotations;
using NCaseFramework.Back.Api.Print;
using NDsl.Back.Api.Def;
using NDsl.Back.Api.Ref;
using NDsl.Back.Api.Util.Table;

namespace NCaseFramework.Back.Imp.Print
{
    public class PrintCaseTableVisitors
        : IPrintCaseTableVisitor<IRefNode<IDefNode>>
    {
        public void Visit(IPrintCaseTableDirector director, IRefNode<IDefNode> node)
        {
            director.Print(node.CodeLocation, new RefColumn(node), "X");
        }

        // THROWS EXCEPTION IF NODE UNKNOWN!
    }

    public class RefColumn : ITableColumn
    {
        [NotNull] private readonly IRefNode<IDefNode> mNode;

        public RefColumn(IRefNode<IDefNode> node)
        {
            mNode = node;
        }

        public HorizontalAlignment HorizontalAlignment
        {
            get { return HorizontalAlignment.Center; }
        }

        public string Title
        {
            get { return string.Format("{0}", mNode.Reference.Id.Name); }
        }

        protected bool Equals(RefColumn other)
        {
            return Equals(mNode.Reference, other.mNode.Reference);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((RefColumn) obj);
        }

        public override int GetHashCode()
        {
            return mNode.Reference.GetHashCode();
        }
    }
}