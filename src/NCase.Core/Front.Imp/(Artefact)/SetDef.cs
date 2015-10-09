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
    public abstract class SetDef<TModel, TId> : Def<TModel, TId>, ISetDef<TModel, TId>, ISetDefModel<TId>
        where TId : ISetDefId
        where TModel : ISetDefModel<TId>
    {
        protected SetDef([NotNull] TId id,
                         [NotNull] IBook book,
                         [NotNull] IServices<TModel> services,
                         [NotNull] ICodeLocationUtil codeLocationUtil)
            : base(id, services, book, codeLocationUtil)
        {
        }
    }
}