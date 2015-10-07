using System;
using JetBrains.Annotations;
using NCase.Front.Ui;
using NDsl.Back.Api.Core;
using NDsl.Front.Api;

namespace NCase.Front.Imp.Op
{
    public class CreateTreeImp : IOperationImp<IBuilder, CreateTree, Builder, ITree>
    {
        private readonly ICodeLocationUtil mCodeLocationUtil;
        private readonly IOperationDirector mOperationDirector;

        public CreateTreeImp([NotNull] ICodeLocationUtil codeLocationUtil, [NotNull] IOperationDirector operationDirector)
        {
            if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");
            if (operationDirector == null) throw new ArgumentNullException("operationDirector");
            mCodeLocationUtil = codeLocationUtil;
            mOperationDirector = operationDirector;
        }

        public ITree Perform(IOperationDirector director, CreateTree uiCreateTree, Builder builder)
        {
            return new Tree(uiCreateTree.Name, builder.Book, mCodeLocationUtil, mOperationDirector);
        }
    }
}