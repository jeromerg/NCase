using System;
using JetBrains.Annotations;
using NDsl.All;
using NDsl.Front.Ui;

namespace NDsl.Front.Imp
{
    public abstract class Artefact<TApi> : IArtefact<TApi>, IArtefactApi<TApi>
        where TApi : IArtefactApi<TApi>
    {
        [NotNull] private readonly IToolBox<TApi> mToolBox;

        protected Artefact(IToolBox<TApi> toolbox)
        {
            mToolBox = toolbox;
        }

        #region IArtefact Implementation

        public abstract TApi Api { get; }

        #endregion

        #region IArtefactApi Implementation

        public IToolBox<TApi> ToolBox
        {
            get { return mToolBox; }
        }

        #endregion
    }
}