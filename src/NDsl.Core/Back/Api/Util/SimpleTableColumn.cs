using System;
using JetBrains.Annotations;

namespace NDsl.Back.Api.Util
{
    public class SimpleTableColumn : ITableColumn
    {
        [NotNull] private readonly string mTitle;
        private readonly HorizontalAlignment mHAlignment;

        public SimpleTableColumn([NotNull] string title, HorizontalAlignment hAlignment = HorizontalAlignment.Left)
        {
            if (title == null) throw new ArgumentNullException("title");
            mTitle = title;
            mHAlignment = hAlignment;
        }

        protected bool Equals(SimpleTableColumn other)
        {
            return string.Equals(mTitle, other.mTitle);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((SimpleTableColumn) obj);
        }

        public HorizontalAlignment HorizontalAlignment
        {
            get { return mHAlignment; }
        }

        public override int GetHashCode()
        {
            return mTitle.GetHashCode();
        }

        [NotNull] public string Title
        {
            get { return mTitle; }
        }
    }
}