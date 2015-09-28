using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCase.Back.Api.Core;
using NCase.Front.Api;
using NDsl.Api.Core;


namespace NCase.Front.Imp
{
    public abstract class DefBase<TDefId, TDef> : IDef<TDef>
        where TDefId : IDefId
    {
        private readonly DefHelper<TDefId, TDef> mDefHelper;

        protected DefBase([NotNull] TDefId defId,
                          [NotNull] string defName,
                          [NotNull] ITokenReaderWriter tokenReaderWriter,
                          [NotNull] IDefHelperFactory defHelperFactory)
        {
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            mDefHelper = defHelperFactory.CreateDefHelper<TDefId, TDef>(defId, defName, tokenReaderWriter);
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

        public TResult Perform<TOp, TResult>(TOp operation) where TOp : IOp<TDef, TResult>
        {
            return mDefHelper.Perform<TOp, TResult>(operation);
        }
    }
}