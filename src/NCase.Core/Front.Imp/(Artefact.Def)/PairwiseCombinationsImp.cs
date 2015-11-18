using JetBrains.Annotations;
using NCaseFramework.Back.Api.Pairwise;
using NCaseFramework.Front.Api.Pairwise;
using NCaseFramework.Front.Ui;
using NDsl.Back.Api.Record;
using NDsl.Back.Api.Util;
using NDsl.Front.Api;

namespace NCaseFramework.Front.Imp
{
    public class PairwiseCombinationsImp
        : SetDefBaseImp<IPairwiseCombinationsModel, PairwiseCombinationsId>,
          PairwiseCombinations,
          IPairwiseCombinationsModel
    {
        public class Factory : IDefFactory<PairwiseCombinations>
        {
            [NotNull] private readonly IServiceSet<IPairwiseCombinationsModel> mServices;
            [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;

            public Factory([NotNull] IServiceSet<IPairwiseCombinationsModel> services,
                           [NotNull] ICodeLocationUtil codeLocationUtil)
            {
                mServices = services;
                mCodeLocationUtil = codeLocationUtil;
            }

            [NotNull]
            public PairwiseCombinations Create([NotNull] string defName, [NotNull] ITokenStream tokenStream)
            {
                return new PairwiseCombinationsImp(defName, tokenStream, mServices, mCodeLocationUtil);
            }
        }

        public PairwiseCombinationsImp([NotNull] string defName,
                                       [NotNull] ITokenStream tokenStream,
                                       [NotNull] IServiceSet<IPairwiseCombinationsModel> services,
                                       [NotNull] ICodeLocationUtil codeLocationUtil)
            : base(new PairwiseCombinationsId(defName), tokenStream, services, codeLocationUtil)
        {
        }

        [NotNull] public override IPairwiseCombinationsModel Model
        {
            get { return this; }
        }
    }
}