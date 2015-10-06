using JetBrains.Annotations;
using NCase.Back.Api.Print;
using NDsl.Back.Api.Core;
using NDsl.Back.Api.RecPlay;
using NDsl.Back.Api.Ref;
using NDsl.Back.Api.Util;

namespace NCase.Back.Imp.Print
{
    public class PrintCaseTableVisitors
        : IPrintCaseTableVisitor<IRefNode<IDefNode>>
    {
        // THROWS EXCEPTION IF NODE UNKNOWN!


                #region inner types

        private class RefColumn : ITableColumn
        {
            [NotNull] private readonly IRefNode<IDefNode> mNode;

            public RefColumn(IRefNode<IDefNode> node)
            {
                mNode = node;
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

            public override string ToString()
            {
                return string.Format("{0} ({1})", mNode.Reference.DefId.Name, mNode.Reference.DefId.DefTypeName);
            }
        }

        #endregion

        public void Visit(IPrintCaseTableDirector director, IRefNode<IDefNode> node)
        {
            director.Print(node.CodeLocation, new RefColumn(node), "X");
        }
    }
}