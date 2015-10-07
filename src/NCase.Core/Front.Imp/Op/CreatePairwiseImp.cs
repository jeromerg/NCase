using System;
using JetBrains.Annotations;
using NCase.Front.Ui;
using NDsl.Back.Api.Core;
using NDsl.Front.Api;

namespace NCase.Front.Imp.Op
{
    public class CreatePairwiseImp : IOperationImp<IBuilder, CreatePairwise, Builder, IPairwise>
    {
        private readonly ICodeLocationUtil mCodeLocationUtil;
        private readonly IOperationDirector mOperationDirector;

        public CreatePairwiseImp([NotNull] ICodeLocationUtil codeLocationUtil, [NotNull] IOperationDirector operationDirector)
        {
            if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");
            if (operationDirector == null) throw new ArgumentNullException("operationDirector");
            mCodeLocationUtil = codeLocationUtil;
            mOperationDirector = operationDirector;
        }

        public IPairwise Perform(IOperationDirector director, CreatePairwise uiCreatePairwise, Builder builder)
        {
            return new Pairwise(uiCreatePairwise.Name, builder.Book, mCodeLocationUtil, mOperationDirector);
        }
    }
}