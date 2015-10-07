using System;
using JetBrains.Annotations;
using NDsl.All;
using NDsl.Back.Api.Core;
using NDsl.Front.Ui;

namespace NDsl.Front.Imp
{
    public abstract class Artefact<TApi> : IArtefact<TApi>, IArtefactApi
        where TApi : IArtefactApi
    {
        [NotNull] private readonly ITools mTools;

        protected Artefact([NotNull] ITools tools)
        {
            if (tools == null) throw new ArgumentNullException("tools");
            mTools = tools;
        }

        [NotNull] protected CodeLocation Location
        {
            get { return Tools<ICodeLocationUtil>().GetCurrentUserCodeLocation(); }
        }

        #region IArtefact

        public abstract TApi Api { get; }

        #endregion

        #region IArtefactApi

        [NotNull]
        public T Tools<T>()
        {
            return mTools.Resolve<T>();
        }

        #endregion
    }
}