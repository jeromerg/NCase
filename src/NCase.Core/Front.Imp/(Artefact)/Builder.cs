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

        public Builder([NotNull] IBook book, [NotNull] IToolBox<IBuilderApi> toolBox)
            : base(toolBox)
        {
            if (book == null) throw new ArgumentNullException("book");
            mBook = book;
        }

        public IBook Book
        {
            get { return mBook; }
        }

        public override IBuilderApi Api
        {
            get { return this; }
        }
    }
}