using System;
using JetBrains.Annotations;
using NDsl.Back.Api.RecPlay;
using NDsl.Back.Api.Util.Table;

namespace NCaseFramework.Back.Imp.InterfaceRecPlay
{
    public class InterfaceRecPlayNodeColumn : ITableColumn
    {
        [NotNull] private readonly IInterfaceRecPlayNode mNode;

        public InterfaceRecPlayNodeColumn([NotNull] IInterfaceRecPlayNode node)
        {
            if (node == null) throw new ArgumentNullException("node");
            mNode = node;
        }

        public HorizontalAlignment HorizontalAlignment
        {
            get { return HorizontalAlignment.Right; }
        }

        [NotNull] public string Title
        {
            get { return mNode.PrintInvocation(); }
        }

        protected bool Equals([NotNull] InterfaceRecPlayNodeColumn other)
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