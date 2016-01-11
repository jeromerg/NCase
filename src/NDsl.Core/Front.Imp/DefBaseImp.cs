using System;
using JetBrains.Annotations;
using NDsl.All.Def;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Def;
using NDsl.Back.Api.Ex;
using NDsl.Back.Api.Record;
using NDsl.Back.Api.Ref;
using NDsl.Back.Api.Util;
using NDsl.Front.Api;
using NDsl.Front.Ui;

namespace NDsl.Front.Imp
{
    public abstract class DefBaseImp<TModel, TId, TDefiner> : ArtefactImp<TModel>, DefBase<TModel, TId, TDefiner>
        where TId : IDefId
        where TModel : IDefModel<TId>
        where TDefiner : Definer
    {
        [NotNull] private readonly TId mId;
        [NotNull] private readonly ITokenStream mTokenStream;
        [NotNull] private readonly ICodeLocationFactory mCodeLocationFactory;
        [NotNull] private readonly ICodeLocationPrinter mCodeLocationPrinter;

        protected DefBaseImp([NotNull] TId id,
                             [NotNull] IServiceSet<TModel> services,
                             [NotNull] ITokenStream tokenStream,
                             [NotNull] ICodeLocationFactory codeLocationFactory,
                             [NotNull] ICodeLocationPrinter codeLocationPrinter)
            : base(services)
        {
            if (id == null) throw new ArgumentNullException("id");
            if (tokenStream == null) throw new ArgumentNullException("tokenStream");
            if (codeLocationFactory == null) throw new ArgumentNullException("codeLocationFactory");
            if (codeLocationPrinter == null) throw new ArgumentNullException("codeLocationPrinter");

            mId = id;
            mTokenStream = tokenStream;
            mCodeLocationFactory = codeLocationFactory;
            mCodeLocationPrinter = codeLocationPrinter;
        }

        private DefState State { get; set; }

        #region IDefModel

        [NotNull] public TId Id
        {
            get { return mId; }
        }

        [NotNull] public ITokenStream TokenStream
        {
            get { return mTokenStream; }
        }

        #endregion

        #region IDef Implementation

        [NotNull]
        public abstract TDefiner Define();

        public void Ref()
        {
            TokenStream.Append(new RefToken<TId>(Id, Loc()));
        }

        #endregion

        #region private methods

        protected void Begin()
        {
            if (State > DefState.NotDefined)
                throw new InvalidSyntaxException(mCodeLocationPrinter, Loc(), "{0} not in 'NotDefined' state", Id);

            State = DefState.Defining;
            TokenStream.SetWriteMode(true);
            TokenStream.Append(CreateBeginToken());
        }

        protected void End()
        {
            if (State != DefState.Defining)
                throw new InvalidSyntaxException(mCodeLocationPrinter, Loc(), "{0} not in 'Defining' state", Id);

            TokenStream.Append(CreateEndToken());
            State = DefState.Defined;
            TokenStream.SetWriteMode(false);
        }

        [NotNull]
        protected virtual IToken CreateBeginToken()
        {
            return new BeginToken<TId>(Id, Loc());
        }

        [NotNull]
        protected virtual IToken CreateEndToken()
        {
            return new EndToken<TId>(Id, Loc());
        }

        [NotNull]
        protected CodeLocation Loc()
        {
            return mCodeLocationFactory.GetCurrentUserCodeLocation();
        }

        #endregion
    }
}