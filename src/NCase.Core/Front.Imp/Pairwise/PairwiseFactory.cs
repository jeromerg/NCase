using System;
using JetBrains.Annotations;
using NCase.Front.Api.Pairwise;
using NCase.Front.Ui;
using NDsl.Back.Api.Book;
using NDsl.Back.Api.Util;

namespace NCase.Front.Imp.Pairwise
{
    public class PairwiseFactory : IPairwiseFactory
    {
        [NotNull] private readonly IServices<IPairwiseModel> mServices;
        [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;

        public PairwiseFactory([NotNull] IServices<IPairwiseModel> services, [NotNull] ICodeLocationUtil codeLocationUtil)
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
}