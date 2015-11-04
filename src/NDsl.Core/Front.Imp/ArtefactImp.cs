using JetBrains.Annotations;
using NDsl.Back.Api.Util;
using NDsl.Front.Api;
using NDsl.Front.Ui;

namespace NDsl.Front.Imp
{
    public abstract class ArtefactImp<TModel> : Artefact<TModel>, IArtefactModel, IApi<TModel>
        where TModel : IArtefactModel
    {
        [NotNull] private readonly IServiceSet<TModel> mServices;

        protected ArtefactImp(IServiceSet<TModel> services)
        {
            mServices = services;
        }

        #region IArtefact Implementation

        public IApi<TModel> Zapi
        {
            get { return this; }
        }

        #endregion

        #region IApi<TModel> Implementation

        public abstract TModel Model { get; }

        public IServiceSet<TModel> Services
        {
            get { return mServices; }
        }

        #endregion
    }
}