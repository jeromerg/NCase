using JetBrains.Annotations;
using NDsl.Back.Api;
using NDsl.Back.Api.Core;
using NDsl.Front.Api;
using NDsl.Front.Imp;
using NDsl.Front.Ui;

namespace NCase.Front.Imp
{
    public abstract class SetDefImpBase<TDef, TDefId, TDefImp>
        : DefImpBase<TDef, TDefId, TDefImp>, ISetDefImp
        where TDef : IDef<TDef>
        where TDefId : IDefId
        where TDefImp : IDefImp<TDefId>

    {
        protected SetDefImpBase([NotNull] ITokenReaderWriter tokenReaderWriter,
                                [NotNull] ICodeLocationUtil codeLocationUtil,
                                [NotNull] IOperationDirector operationDirector)
            : base(tokenReaderWriter, codeLocationUtil, operationDirector)
        {
        }
    }
}