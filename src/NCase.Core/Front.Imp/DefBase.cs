using System;
using NCase.Back.Api.Core;
using NCase.Front.Api;
using NDsl.Back.Api.Core;
using NVisitor.Common.Quality;

namespace NCase.Front.Imp
{
    public abstract class DefBase<TDefId> : IDef
        where TDefId : IDefId
    {
        private readonly DefHelper<TDefId> mDefHelper;

        protected DefBase([NotNull] TDefId defId,
                          [NotNull] string defName,
                          [NotNull] ITokenReaderWriter tokenReaderWriter,
                          [NotNull] IDefHelperFactory defHelperFactory)
        {
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            mDefHelper = defHelperFactory.CreateDefHelper(defId, defName, tokenReaderWriter);
        }

        public virtual ISet Cases
        {
            get { return mDefHelper.Cases; }
        }

        public virtual IDisposable Define()
        {
            return mDefHelper.Define();
        }

        public virtual void Begin()
        {
            mDefHelper.Begin();
        }

        public virtual void End()
        {
            mDefHelper.End();
        }

        public virtual void Ref()
        {
            mDefHelper.Ref();
        }
    }
}