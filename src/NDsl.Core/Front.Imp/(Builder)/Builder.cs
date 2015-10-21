using System;
using JetBrains.Annotations;
using NDsl.Back.Api.Book;
using NDsl.Back.Api.Util;
using NDsl.Front.Api;

namespace NDsl.Front.Imp
{
    public class Builder : Artefact<IBuilderModel>, IBuilder, IBuilderModel
    {
        [NotNull] private readonly ITokenStream mTokenStream;

        public Builder([NotNull] ITokenStream tokenStream, [NotNull] IServices<IBuilderModel> services)
            : base(services)
        {
            if (tokenStream == null) throw new ArgumentNullException("tokenStream");
            mTokenStream = tokenStream;
        }

        public override IBuilderModel Model
        {
            get { return this; }
        }

        #region IBuilderModel

        public ITokenStream TokenStream
        {
            get { return mTokenStream; }
        }

        #endregion
    }
}