using JetBrains.Annotations;
using NCase.Front.Api;
using NCase.Front.Ui;
using NDsl.All;
using NDsl.Back.Api.Core;
using NDsl.Front.Imp;

namespace NCase.Front.Imp
{
    public abstract class SetDef<TDefId, TApi> : Def<TDefId, TApi>, ISetDef<TDefId, TApi>
        where TDefId : IDefId
        where TApi : ISetDefApi<TDefId>

    {
        protected SetDef(TDefId id, [NotNull] IBook book, [NotNull] ITools tools)
            : base(id, book, tools)
        {
        }
    }
}