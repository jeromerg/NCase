using JetBrains.Annotations;
using NCaseFramework.Back.Api.Pairwise;
using NCaseFramework.Front.Api.Pairwise;
using NCaseFramework.Front.Ui;
using NDsl.Back.Api.Record;
using NDsl.Back.Api.Util;
using NDsl.Front.Api;
using NDsl.Front.Imp;
using NDsl.Front.Ui;

namespace NCaseFramework.Front.Imp
{
    public class PairwiseCombinationsImp
        : SetDefBaseImp<IPairwiseCombinationsModel, PairwiseCombinationsId, Definer>,
          PairwiseCombinations,
          IPairwiseCombinationsModel
    {
        public class Factory : IDefFactory<PairwiseCombinations>
        {
            [NotNull] private readonly IServiceSet<IPairwiseCombinationsModel> mServices;
            [NotNull] private readonly ICodeLocationFactory mCodeLocationFactory;
            [NotNull] private readonly ICodeLocationPrinter mCodeLocationPrinter;

            public Factory([NotNull] IServiceSet<IPairwiseCombinationsModel> services,
                           [NotNull] ICodeLocationFactory codeLocationFactory, [NotNull] ICodeLocationPrinter codeLocationPrinter)
            {
                mServices = services;
                mCodeLocationFactory = codeLocationFactory;
                mCodeLocationPrinter = codeLocationPrinter;
            }

            [NotNull]
            public PairwiseCombinations Create([NotNull] string defName, [NotNull] ITokenStream tokenStream)
            {
                return new PairwiseCombinationsImp(defName, tokenStream, mServices, mCodeLocationFactory, mCodeLocationPrinter);
            }
        }

        public PairwiseCombinationsImp([NotNull] string defName,
                                       [NotNull] ITokenStream tokenStream,
                                       [NotNull] IServiceSet<IPairwiseCombinationsModel> services,
                                       [NotNull] ICodeLocationFactory codeLocationFactory, 
                                       [NotNull] ICodeLocationPrinter codeLocationPrinter)
            : base(new PairwiseCombinationsId(defName), tokenStream, services, codeLocationFactory, codeLocationPrinter)
        {
        }

        [NotNull] public override IPairwiseCombinationsModel Model
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