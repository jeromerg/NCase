using System;
using JetBrains.Annotations;
using NCase.Front.Api.Builder;
using NCase.Front.Ui;
using NDsl.Back.Api.Book;
using NDsl.Back.Api.Util;
using NDsl.Front.Imp;

namespace NCase.Front.Imp.Builder
{
    public class Builder : Artefact<IBuilderModel>, IBuilder, IBuilderModel
    {
        [NotNull] private readonly IBook mBook;

        public Builder([NotNull] IBook book, [NotNull] IServices<IBuilderModel> services)
            : base(services)
        {
            if (book == null) throw new ArgumentNullException("book");
            mBook = book;
        }

        public override IBuilderModel Model
        {
            get { return this; }
        }

        #region IBuilderModel

        public IBook Book
        {
            get { return mBook; }
        }

        #endregion
    }
}