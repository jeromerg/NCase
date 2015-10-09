using System;
using JetBrains.Annotations;
using NCase.Front.Api;
using NCase.Front.Ui;
using NDsl.All;
using NDsl.Back.Api.Core;
using NDsl.Front.Imp;

namespace NCase.Front.Imp
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