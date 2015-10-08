using System;
using JetBrains.Annotations;
using NDsl.All;
using NDsl.Back.Api.Block;
using NDsl.Back.Api.Core;
using NDsl.Back.Api.Ref;
using NDsl.Front.Ui;

namespace NDsl.Front.Imp
{
    public abstract class Def<TDefId, TApi> : Artefact<TApi>, IDef<TApi>, IDefApi<TDefId, TApi>
        where TDefId : IDefId
        where TApi : IDefApi<TDefId, TApi>
    {
        private readonly TDefId mId;
        [NotNull] private readonly IBook mBook;
        private readonly ICodeLocationUtil mCodeLocationUtil;

        protected Def([NotNull] TDefId id,
                      [NotNull] IBook book,
                      [NotNull] IToolBox<TApi> toolBox,
                      [NotNull] ICodeLocationUtil codeLocationUtil)
            : base(toolBox)
        {
            if (id == null) throw new ArgumentNullException("id");
            if (book == null) throw new ArgumentNullException("book");
            if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");
            mId = id;
            mBook = book;
            mCodeLocationUtil = codeLocationUtil;
        }

        private DefState State { get; set; }

        #region IDefApi, IDefApi<TDefId>

        IDefId IDefApi.Id
        {
            get { return mId; }
        }

        public TDefId Id
        {
            get { return mId; }
        }

        [NotNull] public IBook Book
        {
            get { return mBook; }
        }

        #endregion

        #region IDef Implementation

        public IDisposable Define()
        {
            return new DisposableWithCallbacks(Begin, End);
        }

        public void Ref()
        {
            Book.Append(new RefToken<TDefId>(Id, Loc()));
        }

        #endregion

        #region private methods

        private void Begin()
        {
            if (State > DefState.NotDefined)
                throw new InvalidSyntaxException(Loc(), "Def {0} not in NotDefined state", Id.Name);

            State = DefState.Defining;
            Book.Append(new BeginToken<TDefId>(Id, Loc()));
        }

        private void End()
        {
            if (State != DefState.Defining)
                throw new InvalidSyntaxException(Loc(), "Def {0} not in Defining state", Id.Name);

            Book.Append(new EndToken<TDefId>(Id, Loc()));
            State = DefState.Defined;
        }

        private CodeLocation Loc()
        {
            return mCodeLocationUtil.GetCurrentUserCodeLocation();
        }

        #endregion
    }
}