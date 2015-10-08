using System;
using JetBrains.Annotations;
using NDsl.All;
using NDsl.Front.Ui;

namespace NDsl.Front.Imp
{
    public abstract class Artefact<TApi> : IArtefact<TApi>, IArtefactApi<TApi>
        where TApi : IArtefactApi<TApi>
    {
        [NotNull] private readonly IToolBox<TApi> mToolbox;

        protected Artefact(IToolBox<TApi> toolbox)
        {
            mToolbox = toolbox;
        }

        #region IArtefact Implementation

        public abstract TApi Api { get; }

        #endregion

        #region IArtefactApi Implementation

        public T Toolbox<T>() //where T : ITool<TApi>
        {
            return mToolbox.GetTool<T>(); 
        }

        #endregion
    }
}