using System;
using JetBrains.Annotations;
using NDsl.All;
using NDsl.Back.Api.Block;
using NDsl.Back.Api.Core;
using NDsl.Back.Api.Ref;
using NDsl.Front.Ui;

namespace NDsl.Front.Imp
{
    public abstract class Def<TDefId, TApi> : Artefact<TApi>, IDef<TDefId, TApi>, IDefApi<TDefId>
        where TDefId : IDefId
        where TApi : IDefApi<TDefId>
    {
        private readonly TDefId mId;
        [NotNull] private readonly IBook mBook;

        protected Def([NotNull] TDefId id, [NotNull] IBook book, [NotNull] ITools tools)
            : base(tools)
        {
            if (id == null) throw new ArgumentNullException("id");
            if (book == null) throw new ArgumentNullException("book");
            mId = id;
            mBook = book;
        }

        private DefState State { get; set; }

        #region IDefApi<TDefId>

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
            Book.Append(new RefToken<TDefId>(Id, Location));
        }

        #endregion

        #region private methods

        private void Begin()
        {
            if (State > DefState.NotDefined)
                throw new InvalidSyntaxException(Location, "Def {0} not in NotDefined state", DefId.Name);

            State = DefState.Defining;
            Book.Append(new BeginToken<TDefId>(Id, Location));
        }

        private void End()
        {
            if (State != DefState.Defining)
                throw new InvalidSyntaxException(Location, "Def {0} not in Defining state", DefId.Name);

            Book.Append(new EndToken<TDefId>(Id, Location));
            State = DefState.Defined;
        }

        #endregion

    }
}