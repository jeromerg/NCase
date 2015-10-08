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
    public abstract class SetDef<TDefId, TApi> : Def<TDefId, TApi>, ISetDef<TApi>
        where TDefId : ISetDefId
        where TApi : ISetDefApi<TDefId, TApi>

    {
        protected SetDef(TDefId id,
                         [NotNull] IBook book,
                         [NotNull] IToolBox<TApi> toolBox,
                         [NotNull] ICodeLocationUtil codeLocationUtil)
            : base(id, book, toolBox, codeLocationUtil)
        {
            if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");
        }
    }
}