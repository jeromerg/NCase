using System;
using JetBrains.Annotations;
using NCase.Back.Api.Pairwise;
using NCase.Front.Ui;
using NDsl.Back.Api.Core;
using NDsl.Front.Api;
using NDsl.Front.Imp;

namespace NCase.Front.Imp
{
    public class Pairwise : SetDefImpBase<IPairwise, PairwiseId, Pairwise>, IPairwise, IDefImp<PairwiseId>
    {
        #region inner types

        public class Factory : IDefFactory<IPairwise>
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

            public IPairwise Create([NotNull] ITokenReaderWriter tokenReaderWriter, [NotNull] string name)
            {
                return new Pairwise(name, tokenReaderWriter, mCodeLocationUtil, mOperationDirector);
            }
        }

        #endregion

        private readonly PairwiseId mId;

        public Pairwise([NotNull] string defName,
                        [NotNull] ITokenReaderWriter tokenReaderWriter,
                        [NotNull] ICodeLocationUtil codeLocationUtil,
                        [NotNull] IOperationDirector operationDirector)
            : base(defName, tokenReaderWriter, codeLocationUtil, operationDirector)
        {
            mId = new PairwiseId();
        }

        protected override Pairwise ThisDefImpl
        {
            get { return this; }
        }

        public PairwiseId Id
        {
            get { return mId; }
        }
    }
}