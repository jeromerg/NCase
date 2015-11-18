using System;
using JetBrains.Annotations;
using NDsl.Back.Api.Builder;
using NDsl.Back.Api.Record;
using NDsl.Back.Api.RecPlay;
using NDsl.Back.Api.Util;
using NDsl.Front.Api;

namespace NDsl.Front.Imp
{
    public class CaseBuilder : ArtefactImp<ICaseBuilderModel>, Api.CaseBuilder, ICaseBuilderModel
    {
        [NotNull] private readonly ITokenStream mTokenStream;
        private RecPlayMode mRecPlayMode;

        public CaseBuilder([NotNull] ITokenStream tokenStream, [NotNull] IServiceSet<ICaseBuilderModel> services)
            : base(services)
        {
            if (tokenStream == null) throw new ArgumentNullException("tokenStream");
            mTokenStream = tokenStream;
        }

        public override ICaseBuilderModel Model
        {
            get { return this; }
        }

        #region ICaseBuilderModel

        public ITokenStream TokenStream
        {
            get { return mTokenStream; }
        }

        public RecPlayMode RecPlayMode
        {
            get { return mRecPlayMode; }
            set
            {
                if (mRecPlayMode == RecPlayMode.Recorded)
                    throw new IndexOutOfRangeException("Recorded state is the initial state and cannot be set (should never happen)");

                mRecPlayMode = value;
            }
        }

        #endregion
    }
}