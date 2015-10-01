using System;
using JetBrains.Annotations;
using NCase.Back.Api.Seq;
using NCase.Front.Ui;
using NDsl.Back.Api.Core;
using NDsl.Front.Api;

namespace NCase.Front.Imp
{
    public class Seq : SetDefImpBase<ISeq, SeqId, Seq>, ISeq, IDefImp<SeqId>
    {
        #region inner types

        public class Factory : IDefFactory<ISeq>
        {
            private readonly ICodeLocationUtil mCodeLocationUtil;
            private readonly IOperationDirector mOperationDirector;

            public Factory([NotNull] ICodeLocationUtil codeLocationUtil, [NotNull] IOperationDirector operationDirector)
            {
                if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");
                if (operationDirector == null) throw new ArgumentNullException("operationDirector");
                mCodeLocationUtil = codeLocationUtil;
                mOperationDirector = operationDirector;
            }

            public ISeq Create([NotNull] ITokenReaderWriter tokenReaderWriter, [NotNull] string name)
            {
                return new Seq(name, tokenReaderWriter, mCodeLocationUtil, mOperationDirector);
            }
        }

        #endregion

        [NotNull] private readonly SeqId mId;

        public Seq([NotNull] string defName,
                    [NotNull] ITokenReaderWriter tokenReaderWriter,
                    [NotNull] ICodeLocationUtil codeLocationUtil,
                    [NotNull] IOperationDirector operationDirector)
            : base(tokenReaderWriter, codeLocationUtil, operationDirector)
        {
            mId = new SeqId(defName);
        }

        protected override Seq ThisDefImpl
        {
            get { return this; }
        }

        public SeqId Id
        {
            get { return mId; }
        }
    }
}