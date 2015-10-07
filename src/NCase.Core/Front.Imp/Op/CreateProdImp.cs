using System;
using JetBrains.Annotations;
using NCase.Front.Ui;
using NDsl.Back.Api.Core;
using NDsl.Front.Api;

namespace NCase.Front.Imp.Op
{
    public class CreateProdImp : IOperationImp<IBuilder, CreateProd, Builder, IProd>
    {
        private readonly ICodeLocationUtil mCodeLocationUtil;
        private readonly IOperationDirector mOperationDirector;

        public CreateProdImp([NotNull] ICodeLocationUtil codeLocationUtil, [NotNull] IOperationDirector operationDirector)
        {
            if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");
            if (operationDirector == null) throw new ArgumentNullException("operationDirector");
            mCodeLocationUtil = codeLocationUtil;
            mOperationDirector = operationDirector;
        }

        public IProd Perform(IOperationDirector director, CreateProd uiCreateProd, Builder builder)
        {
            return new Prod(uiCreateProd.Name, builder.Book, mCodeLocationUtil, mOperationDirector);
        }
    }
}