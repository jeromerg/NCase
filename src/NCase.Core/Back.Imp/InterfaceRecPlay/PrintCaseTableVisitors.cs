using JetBrains.Annotations;
using NCase.Back.Api.Print;
using NDsl.Back.Api.RecPlay;
using NDsl.Back.Api.Util;

namespace NCase.Back.Imp.InterfaceRecPlay
{
    public class PrintCaseTableVisitors : IPrintCaseTableVisitor<IInterfaceRecPlayNode>
    {
        #region inner types

        private class InterfaceRecPlayNodeColumn : ITableColumn
        {
            [NotNull] private readonly IInterfaceRecPlayNode mNode;

            public InterfaceRecPlayNodeColumn(IInterfaceRecPlayNode node)
            {
                mNode = node;
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

            public override string ToString()
            {
                return mNode.PrintInvocation();
            }
        }

        #endregion

        public void Visit(IPrintCaseTableDirector director, IInterfaceRecPlayNode node)
        {
            director.Print(node.CodeLocation,
                           new InterfaceRecPlayNodeColumn(node),
                           node.PropertyValue != null ? node.PropertyValue.ToString() : "null");
        }
    }
}