using System;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.Prod;
using NCaseFramework.Front.Api.Prod;
using NCaseFramework.Front.Ui;
using NDsl.Back.Api.Record;
using NDsl.Back.Api.Util;
using NDsl.Front.Api;

namespace NCaseFramework.Front.Imp
{
    public class AllCombinationsImp
        : SetDefBaseImp<IAllCombinationsModel, AllCombinationsId>,
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
                if (services == null) throw new ArgumentNullException("services");
                if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");
                mServices = services;
                mCodeLocationUtil = codeLocationUtil;
            }

            public AllCombinations Create(string defName, ITokenStream tokenStream)
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

        public override IAllCombinationsModel Model
        {
            get { return this; }
        }
    }
}