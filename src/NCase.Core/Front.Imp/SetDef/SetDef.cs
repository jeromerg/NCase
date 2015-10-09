using JetBrains.Annotations;
using NCase.Back.Api.SetDef;
using NCase.Front.Api.SetDef;
using NCase.Front.Ui;
using NDsl.Back.Api.Book;
using NDsl.Back.Api.Util;
using NDsl.Front.Imp;

namespace NCase.Front.Imp.SetDef
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