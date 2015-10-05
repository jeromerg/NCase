﻿using System;
using JetBrains.Annotations;
using NCase.Back.Api.Prod;
using NCase.Front.Ui;
using NDsl.Back.Api.Core;
using NDsl.Front.Api;

namespace NCase.Front.Imp
{
    public class ProdImp : SetDefImpBase<IProd, ProdId, ProdImp>, IProd, IDefImp<ProdId>
    {
        #region inner types

        public class Factory : IDefFactory<IProd>
        {
            [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;
            [NotNull] private readonly IOperationDirector mOperationDirector;

            public Factory([NotNull] ICodeLocationUtil codeLocationUtil, [NotNull] IOperationDirector operationDirector)
            {
                if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");
                if (operationDirector == null) throw new ArgumentNullException("operationDirector");
                mCodeLocationUtil = codeLocationUtil;
                mOperationDirector = operationDirector;
            }

            public virtual IProd Create([NotNull] ITokenReaderWriter tokenReaderWriter, [NotNull] string name)
            {
                return new ProdImp(name, tokenReaderWriter, mCodeLocationUtil, mOperationDirector);
            }
        }

        #endregion

        [NotNull] private readonly ProdId mId;

        public ProdImp([NotNull] string defName,
                    [NotNull] ITokenReaderWriter tokenReaderWriter,
                    [NotNull] ICodeLocationUtil codeLocationUtil,
                    [NotNull] IOperationDirector operationDirector)
            : base(tokenReaderWriter, codeLocationUtil, operationDirector)
        {
            mId = new ProdId(defName);
        }

        protected override ProdImp ThisDefImpl
        {
            get { return this; }
        }

        public ProdId Id
        {
            get { return mId; }
        }
    }
}