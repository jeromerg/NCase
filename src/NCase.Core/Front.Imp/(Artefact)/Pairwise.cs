using System;
using JetBrains.Annotations;
using NCase.Back.Api.Pairwise;
using NCase.Front.Api;
using NCase.Front.Ui;
using NDsl.All;
using NDsl.Back.Api.Core;
using NDsl.Front.Ui;

namespace NCase.Front.Imp
{
    public class Pairwise : SetDef<IPairwiseModel, PairwiseId>, IPairwise, IPairwiseModel
    {
        public class Factory : IPairwiseFactory
        {
            [NotNull] private readonly IServices<IPairwiseModel> mServices;
            [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;

            public Factory([NotNull] IServices<IPairwiseModel> services, [NotNull] ICodeLocationUtil codeLocationUtil)
            {
                if (services == null) throw new ArgumentNullException("services");
                if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");
                mServices = services;
                mCodeLocationUtil = codeLocationUtil;
            }

            public IPairwise Create(string defName, IBook book)
            {
                return new Pairwise(defName, book, mServices, mCodeLocationUtil);
            }
        }

        public Pairwise([NotNull] string defName, [NotNull] IBook book, [NotNull] IServices<IPairwiseModel> services,
                        [NotNull] ICodeLocationUtil codeLocationUtil)
            : base(new PairwiseId(defName), book, services, codeLocationUtil)
        {
            if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");
        }

        public override IPairwiseModel Model
        {
            get { return this; }
        }

    }
}