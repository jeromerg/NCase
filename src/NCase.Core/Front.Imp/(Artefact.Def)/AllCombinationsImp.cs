using JetBrains.Annotations;
using NCaseFramework.Back.Api.Prod;
using NCaseFramework.Front.Api.Prod;
using NCaseFramework.Front.Ui;
using NDsl.Back.Api.Record;
using NDsl.Back.Api.Util;
using NDsl.Front.Api;
using NDsl.Front.Imp;
using NDsl.Front.Ui;

namespace NCaseFramework.Front.Imp
{
    public class AllCombinationsImp
        : SetDefBaseImp<IAllCombinationsModel, AllCombinationsId, Definer>,
          AllCombinations,
          IAllCombinationsModel
    {
        public class Factory : IDefFactory<AllCombinations>
        {
            [NotNull] private readonly IServiceSet<IAllCombinationsModel> mServices;
            [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;

            public Factory([NotNull] IServiceSet<IAllCombinationsModel> services,
                           [NotNull] ICodeLocationUtil codeLocationUtil)
            {
                mServices = services;
                mCodeLocationUtil = codeLocationUtil;
            }

            [NotNull]
            public AllCombinations Create([NotNull] string defName, [NotNull] ITokenStream tokenStream)
            {
                return new AllCombinationsImp(defName, tokenStream, mServices, mCodeLocationUtil);
            }
        }

        public AllCombinationsImp([NotNull] string defName,
                                  [NotNull] ITokenStream tokenStream,
                                  [NotNull] IServiceSet<IAllCombinationsModel> services,
                                  [NotNull] ICodeLocationUtil codeLocationUtil)
            : base(new AllCombinationsId(defName), tokenStream, services, codeLocationUtil)
        {
        }

        [NotNull] public override IAllCombinationsModel Model
        {
            get { return this; }
        }

        [NotNull]
        public override Definer Define()
        {
            return new DefinerImp(Begin, End);
        }
    }
}