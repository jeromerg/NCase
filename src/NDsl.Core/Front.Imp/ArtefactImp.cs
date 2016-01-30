using JetBrains.Annotations;
using NDsl.Back.Api.Util;
using NDsl.Front.Api;
using NDsl.Front.Ui;

namespace NDsl.Front.Imp
{
    public abstract class ArtefactImp<TModel> : Artefact<TModel>, IApi<TModel>
    {
        [NotNull] private readonly IServiceSet<TModel> mServices;

        protected ArtefactImp([NotNull] IServiceSet<TModel> services)
        {
            mServices = services;
        }

        #region IArtefact Implementation

        [NotNull] public IApi<TModel> Api
        {
            get { return this; }
        }

        #endregion

        #region IApi<TModel> Implementation

        [NotNull] public abstract TModel Model { get; }

        [NotNull] public IServiceSet<TModel> Services
        {
            get { return mServices; }
        }

        #endregion
    }
}