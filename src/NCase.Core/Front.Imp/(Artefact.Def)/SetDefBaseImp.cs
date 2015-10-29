using JetBrains.Annotations;
using NCaseFramework.Back.Api.SetDef;
using NCaseFramework.Front.Api.SetDef;
using NCaseFramework.Front.Ui;
using NDsl.Back.Api.Book;
using NDsl.Back.Api.Util;
using NDsl.Front.Imp;

namespace NCaseFramework.Front.Imp
{
    public abstract class SetDefBaseImp<TModel, TId> : DefBaseImp<TModel, TId>, SetDefBase<TModel, TId>, ISetDefModel<TId>
        where TId : ISetDefId
        where TModel : ISetDefModel<TId>
    {
        protected SetDefBaseImp([NotNull] TId id,
                                [NotNull] ITokenStream tokenStream,
                                [NotNull] IServiceSet<TModel> services,
                                [NotNull] ICodeLocationUtil codeLocationUtil)
            : base(id, services, tokenStream, codeLocationUtil)
        {
        }
    }
}