using System;
using JetBrains.Annotations;
using NCase.Front.Ui;
using NDsl.Back.Api.Core;
using NDsl.Front.Api;

namespace NCase.Front.Imp.Op
{
    public class CreateSeqImp : IOperationImp<IBuilder, CreateSeq, Builder, ISeq>
    {
        private readonly ICodeLocationUtil mCodeLocationUtil;
        private readonly IOperationDirector mOperationDirector;

        public CreateSeqImp([NotNull] ICodeLocationUtil codeLocationUtil, [NotNull] IOperationDirector operationDirector)
        {
            if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");
            if (operationDirector == null) throw new ArgumentNullException("operationDirector");
            mCodeLocationUtil = codeLocationUtil;
            mOperationDirector = operationDirector;
        }

        public ISeq Perform(IOperationDirector director, CreateSeq uiCreateSeq, Builder builder)
        {
            return new Seq(uiCreateSeq.Name, builder.Book, mCodeLocationUtil, mOperationDirector);
        }
    }
}