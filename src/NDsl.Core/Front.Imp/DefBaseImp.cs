using System;
using JetBrains.Annotations;
using NDsl.All.Def;
using NDsl.Back.Api.Def;
using NDsl.Back.Api.Ex;
using NDsl.Back.Api.Ref;
using NDsl.Back.Api.TokenStream;
using NDsl.Back.Api.Util;
using NDsl.Front.Api;
using NDsl.Front.Ui;

namespace NDsl.Front.Imp
{
    public abstract class DefBaseImp<TModel, TId> : ArtefactImp<TModel>, DefBase<TModel, TId>
        where TId : IDefId
        where TModel : IDefModel<TId>
    {
        private readonly TId mId;
        [NotNull] private readonly ITokenStream mTokenStream;
        private readonly ICodeLocationUtil mCodeLocationUtil;

        protected DefBaseImp([NotNull] TId id,
                             [NotNull] IServiceSet<TModel> services,
                             [NotNull] ITokenStream tokenStream,
                             [NotNull] ICodeLocationUtil codeLocationUtil)
            : base(services)
        {
            if (id == null) throw new ArgumentNullException("id");
            if (tokenStream == null) throw new ArgumentNullException("tokenStream");
            if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");
            mId = id;
            mTokenStream = tokenStream;
            mCodeLocationUtil = codeLocationUtil;
        }

        private DefState State { get; set; }

        #region IDefModel

        public TId Id
        {
            get { return mId; }
        }

        [NotNull] public ITokenStream TokenStream
        {
            get { return mTokenStream; }
        }

        #endregion

        #region IDef Implementation

        public IDisposable Define()
        {
            return new DisposableWithCallbacks(Begin, End);
        }

        public void Ref()
        {
            TokenStream.Append(new RefToken<TId>(Id, Loc()));
        }

        #endregion

        #region private methods

        private void Begin()
        {
            if (State > DefState.NotDefined)
                throw new InvalidSyntaxException(Loc(), "{0} not in 'NotDefined' state", Id);

            State = DefState.Defining;
            TokenStream.SetWriteMode(true);
            TokenStream.Append(new BeginToken<TId>(Id, Loc()));
        }

        private void End()
        {
            if (State != DefState.Defining)
                throw new InvalidSyntaxException(Loc(), "{0} not in 'Defining' state", Id);

            TokenStream.Append(new EndToken<TId>(Id, Loc()));
            State = DefState.Defined;
            TokenStream.SetWriteMode(false);
        }

        private CodeLocation Loc()
        {
            return mCodeLocationUtil.GetCurrentUserCodeLocation();
        }

        #endregion
    }
}