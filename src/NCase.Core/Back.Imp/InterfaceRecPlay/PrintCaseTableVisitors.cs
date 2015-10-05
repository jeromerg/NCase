using JetBrains.Annotations;
using NCase.Back.Api.Print;
using NDsl.Back.Api.Core;
using NDsl.Back.Api.RecPlay;

namespace NCase.Back.Imp.RecPlay
{
    public class PrintCaseTableVisitors : IPrintCaseTableVisitor<IInterfaceRecPlayNode>
    {
        private class InterfaceRecPlayNodeColumn
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
                if (obj.GetType() != this.GetType()) return false;
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
        public void Visit(IPrintCaseTableDirector director, IInterfaceRecPlayNode node)
        {
            director.Print(node.CodeLocation, new InterfaceRecPlayNodeColumn(node), node.PropertyValue != null ? node.PropertyValue.ToString() : "null");
        }
    }
}