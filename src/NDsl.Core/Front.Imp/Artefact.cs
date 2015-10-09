using JetBrains.Annotations;
using NDsl.Back.Api.Util;
using NDsl.Front.Api;

namespace NDsl.Front.Imp
{
    public abstract class Artefact<TModel> : IArtefact<TModel>, IArtefactModel, IApi<TModel>
        where TModel : IArtefactModel
    {
        [NotNull] private readonly IServices<TModel> mServices;

        protected Artefact(IServices<TModel> toolbox)
        {
            mServices = toolbox;
        }

        #region IArtefact Implementation

        public IApi<TModel> Api
        {
            get { return this; }
        }

        #endregion

        #region IApi<TModel> Implementation

        public abstract TModel Model { get; }

        public IServices<TModel> Services
        {
            get { return mServices; }
        }

        #endregion
    }
}