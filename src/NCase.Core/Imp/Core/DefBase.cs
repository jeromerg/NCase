using System;
using NCase.Api.Dev.Core;
using NCase.Api.Pub;
using NDsl.Api.Dev.Core;
using NVisitor.Common.Quality;

namespace NCase.Imp.Core
{
    public abstract class DefBase<TDef> : IDef
        where TDef : IDef
    {
        private readonly DefHelper<TDef> mDefHelper;

        protected DefBase([NotNull] string defName,
                          [NotNull] ITokenReaderWriter tokenReaderWriter,
                          [NotNull] IDefHelperFactory defHelperFactory)
        {
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            mDefHelper = defHelperFactory.CreateDefHelper(GetDef(), defName, tokenReaderWriter);
        }

        public TResult Get<TResult>(ITransform<IDef, TResult> transform)
        {
            return mDefHelper.Get(transform);
        }

        public virtual ISet Cases
        {
            get { return mDefHelper.Cases; }
        }

        protected abstract TDef GetDef();

        public IDisposable Define()
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