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

        public HorizontalAlignment HorizontalAlignment
        {
            get { return mHAlignment; }
        }

        [NotNull] public string Title
        {
            get { return mTitle; }
        }

        protected bool Equals(SimpleTableColumn other)
        {
            return string.Equals(mTitle, other.mTitle);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((SimpleTableColumn) obj);
        }

        public override int GetHashCode()
        {
            return mTitle.GetHashCode();
        }
    }
}