using JetBrains.Annotations;
using NCaseFramework.Back.Api.Combinations;
using NCaseFramework.Front.Api.Combinations;
using NCaseFramework.Front.Ui;
using NDsl.Back.Api.Record;
using NDsl.Back.Api.Util;
using NDsl.Front.Api;

namespace NCaseFramework.Front.Imp
{
    public class CombinationsImp : SetDefBaseImp<ICombinationsModel, CombinationsId, CombinationsDefiner>, Combinations, ICombinationsModel
    {
        public class Factory : IDefFactory<Combinations>
        {
            [NotNull]
            private readonly IServiceSet<ICombinationsModel> mServices;
            [NotNull] private readonly ICodeLocationFactory mCodeLocationFactory;
            [NotNull] private readonly ICodeLocationPrinter mCodeLocationPrinter;

            public Factory([NotNull] IServiceSet<ICombinationsModel> services, [NotNull] ICodeLocationFactory codeLocationFactory, [NotNull] ICodeLocationPrinter codeLocationPrinter)
            {
                mServices = services;
                mCodeLocationFactory = codeLocationFactory;
                mCodeLocationPrinter = codeLocationPrinter;
            }

            [NotNull]
            public Combinations Create([NotNull] string defName, [NotNull] ITokenStream tokenStream)
            {
                return new CombinationsImp(defName, tokenStream, mServices, mCodeLocationFactory, mCodeLocationPrinter);
            }
        }

        public CombinationsImp([NotNull] string defName,
                       [NotNull] ITokenStream tokenStream,
                       [NotNull] IServiceSet<ICombinationsModel> services,
                       [NotNull] ICodeLocationFactory codeLocationFactory, 
                       [NotNull] ICodeLocationPrinter codeLocationPrinter)
            : base(new CombinationsId(defName), tokenStream, services, codeLocationFactory, codeLocationPrinter)
        {
        }

        public override ICombinationsModel Model
        {
            get { return this; }
        }

        [NotNull]
        public override CombinationsDefiner Define()
        {
            return new CombinationsDefinerImp(TokenStream, Begin, End);
        }
    }
}