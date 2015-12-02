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
            [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;

            public Factory([NotNull] IServiceSet<ICombinationsModel> services, [NotNull] ICodeLocationUtil codeLocationUtil)
            {
                mServices = services;
                mCodeLocationUtil = codeLocationUtil;
            }

            [NotNull]
            public Combinations Create([NotNull] string defName, [NotNull] ITokenStream tokenStream)
            {
                return new CombinationsImp(defName, tokenStream, mServices, mCodeLocationUtil);
            }
        }

        public CombinationsImp([NotNull] string defName,
                       [NotNull] ITokenStream tokenStream,
                       [NotNull] IServiceSet<ICombinationsModel> services,
                       [NotNull] ICodeLocationUtil codeLocationUtil)
            : base(new CombinationsId(defName), tokenStream, services, codeLocationUtil)
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