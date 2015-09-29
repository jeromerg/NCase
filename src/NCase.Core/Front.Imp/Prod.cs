﻿using System;
using JetBrains.Annotations;
using NCase.Back.Api.Prod;
using NCase.Front.Api;
using NDsl.Back.Api.Core;
using NDsl.Front.Imp;
using NDsl.Front.Imp.Op;

namespace NCase.Front.Imp
{
    public class Prod : SetDefImpBase<IProd, ProdId, Prod>, IProd, IDefImp<ProdId>
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

            public IProd Create([NotNull] ITokenReaderWriter tokenReaderWriter, [NotNull] string name)
            {
                return new Prod(name, tokenReaderWriter, mCodeLocationUtil, mOperationDirector);
            }
        }

        #endregion

        [NotNull] private readonly ProdId mId;

        public Prod([NotNull] string defName,
                    [NotNull] ITokenReaderWriter tokenReaderWriter,
                    [NotNull] ICodeLocationUtil codeLocationUtil,
                    [NotNull] IOperationDirector operationDirector)
            : base(defName, tokenReaderWriter, codeLocationUtil, operationDirector)
        {
            mId = new ProdId();
        }

        protected override Prod ThisDefImpl
        {
            get { return this; }
        }

        public ProdId Id
        {
            get { return mId; }
        }
    }
}