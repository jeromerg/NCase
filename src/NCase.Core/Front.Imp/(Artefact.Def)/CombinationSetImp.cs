using JetBrains.Annotations;
using NCaseFramework.Back.Api.CombinationSet;
using NCaseFramework.Front.Api.CombinationSet;
using NCaseFramework.Front.Ui;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Record;
using NDsl.Back.Api.Util;
using NDsl.Front.Api;

namespace NCaseFramework.Front.Imp
{
    public class CombinationSetImp : SetDefBaseImp<ICombinationSetModel, CombinationSetId, CombinationSetDefiner>, CombinationSet, ICombinationSetModel
    {
        public class Factory : IDefFactory<CombinationSet>
        {
            [NotNull]
            private readonly IServiceSet<ICombinationSetModel> mServices;
            [NotNull] private readonly ICodeLocationFactory mCodeLocationFactory;
            [NotNull] private readonly ICodeLocationPrinter mCodeLocationPrinter;

            public Factory([NotNull] IServiceSet<ICombinationSetModel> services, [NotNull] ICodeLocationFactory codeLocationFactory, [NotNull] ICodeLocationPrinter codeLocationPrinter)
            {
                mServices = services;
                mCodeLocationFactory = codeLocationFactory;
                mCodeLocationPrinter = codeLocationPrinter;
            }

            [NotNull]
            public CombinationSet Create([NotNull] string defName, [NotNull] ITokenStream tokenStream)
            {
                return new CombinationSetImp(defName, tokenStream, mServices, mCodeLocationFactory, mCodeLocationPrinter);
            }
        }

        public CombinationSetImp([NotNull] string defName,
                       [NotNull] ITokenStream tokenStream,
                       [NotNull] IServiceSet<ICombinationSetModel> services,
                       [NotNull] ICodeLocationFactory codeLocationFactory, 
                       [NotNull] ICodeLocationPrinter codeLocationPrinter)
            : base(new CombinationSetId(defName), tokenStream, services, codeLocationFactory, codeLocationPrinter)
        {
        }

        public override ICombinationSetModel Model
        {
            get { return this; }
        }

        public bool IsOnlyPairwiseProduct { get; set; }

        [NotNull]
        public override CombinationSetDefiner Define()
        {
            return new CombinationSetDefinerImp(TokenStream, CodeLocationFactory, Begin, End);
        }

        protected override IToken CreateBeginToken()
        {
            return new CombinationSetBeginToken(Id, GetCodeLocation(), IsOnlyPairwiseProduct);
        }
    }
}