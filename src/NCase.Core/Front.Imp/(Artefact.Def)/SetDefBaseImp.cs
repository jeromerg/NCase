using JetBrains.Annotations;
using NCaseFramework.Back.Api.SetDef;
using NCaseFramework.Front.Api.SetDef;
using NCaseFramework.Front.Ui;
using NDsl.Back.Api.Record;
using NDsl.Back.Api.Util;
using NDsl.Front.Imp;
using NDsl.Front.Ui;

namespace NCaseFramework.Front.Imp
{
    public abstract class SetDefBaseImp<TModel, TId, TDefiner>
        : DefBaseImp<TModel, TId, TDefiner>,
          SetDefBase<TModel, TId, TDefiner>,
          ISetDefModel<TId>
        where TId : ISetDefId
        where TModel : ISetDefModel<TId>
        where TDefiner : Definer
    {
        protected SetDefBaseImp([NotNull] TId id,
                                [NotNull] ITokenStream tokenStream,
                                [NotNull] IServiceSet<TModel> services,
                                [NotNull] ICodeLocationFactory codeLocationFactory,
                                [NotNull] ICodeLocationPrinter codeLocationPrinter)
            : base(id, services, tokenStream, codeLocationFactory, codeLocationPrinter)
        {
        }
    }
}