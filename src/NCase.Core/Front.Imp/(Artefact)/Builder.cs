using System;
using JetBrains.Annotations;
using NCase.Front.Api;
using NCase.Front.Ui;
using NDsl.All;
using NDsl.Back.Api.Core;
using NDsl.Front.Imp;

namespace NCase.Front.Imp
{
    public class Builder : Artefact<IBuilderApi>, IBuilder, IBuilderApi
    {
        [NotNull] private readonly IBook mBook;

        public Builder([NotNull] IBook book, ITools tools)
            : base(tools)
        {
            if (book == null) throw new ArgumentNullException("book");
            mBook = book;
        }

        [NotNull] public IBook Book
        {
            get { return mBook; }
        }

        public override IBuilderApi Api
        {
            get { return this; }
        }
    }
}