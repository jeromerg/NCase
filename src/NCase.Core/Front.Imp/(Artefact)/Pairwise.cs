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
    public class Pairwise : SetDef<IPairwiseApi, PairwiseId>, IPairwise, IPairwiseApi
    {
        public class Factory : IPairwiseFactory
        {
            [NotNull] private readonly IToolBox<IPairwiseApi> mToolBox;
            [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;

            public Factory([NotNull] IToolBox<IPairwiseApi> toolBox, [NotNull] ICodeLocationUtil codeLocationUtil)
            {
                if (toolBox == null) throw new ArgumentNullException("toolBox");
                if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");
                mToolBox = toolBox;
                mCodeLocationUtil = codeLocationUtil;
            }

            public IPairwise Create(string defName, IBook book)
            {
                return new Pairwise(defName, book, mToolBox, mCodeLocationUtil);
            }
        }

        public Pairwise([NotNull] string defName, [NotNull] IBook book, [NotNull] IToolBox<IPairwiseApi> toolBox,
                        [NotNull] ICodeLocationUtil codeLocationUtil)
            : base(new PairwiseId(defName), book, toolBox, codeLocationUtil)
        {
            if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");
        }

        public override IPairwiseApi Api
        {
            get { return this; }
        }
    }
}