using System;
using JetBrains.Annotations;
using NCase.Back.Api.Tree;
using NCase.Front.Api;
using NCase.Front.Ui;
using NDsl.All;
using NDsl.Back.Api.Core;
using NDsl.Front.Imp;

namespace NCase.Front.Imp
{
    public abstract class SetDef<TApi, TId> : Def<TApi, TId>, ISetDef<TApi>, ISetDefApi<TApi, TId>
        where TId : ISetDefId
        where TApi : ISetDefApi<TApi, TId>
    {
        protected SetDef([NotNull] TId id,
                         [NotNull] IBook book,
                         [NotNull] IToolBox<TApi> toolBox,
                         [NotNull] ICodeLocationUtil codeLocationUtil)
            : base(id, toolBox, book, codeLocationUtil)
        {
        }
    }
}