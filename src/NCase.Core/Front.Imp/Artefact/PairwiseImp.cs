using System;
using JetBrains.Annotations;
using NCase.Back.Api.Pairwise;
using NCase.Front.Ui;
using NDsl.Back.Api.Core;
using NDsl.Front.Api;

namespace NCase.Front.Imp.Artefact
{
    public class PairwiseImp : SetDefImpBase<IPairwise, PairwiseId, PairwiseImp>, IPairwise, IDefImp<PairwiseId>
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

            public virtual IPairwise Create([NotNull] ITokenReaderWriter tokenReaderWriter, [NotNull] string name)
            {
                return new PairwiseImp(name, tokenReaderWriter, mCodeLocationUtil, mOperationDirector);
            }
        }

        #endregion

        [NotNull] private readonly PairwiseId mId;

        public PairwiseImp([NotNull] string defName,
                           [NotNull] ITokenReaderWriter tokenReaderWriter,
                           [NotNull] ICodeLocationUtil codeLocationUtil,
                           [NotNull] IOperationDirector operationDirector)
            : base(tokenReaderWriter, codeLocationUtil, operationDirector)
        {
            mId = new PairwiseId(defName);
        }

        protected override PairwiseImp ThisDefImpl
        {
            get { return this; }
        }

        public PairwiseId Id
        {
            get { return mId; }
        }
    }
}