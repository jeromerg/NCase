using System;
using JetBrains.Annotations;
using NCase.Front.Api.Prod;
using NCase.Front.Ui;
using NDsl.Back.Api.Book;
using NDsl.Back.Api.Util;

namespace NCase.Front.Imp.Prod
{
    public class ProdFactory : IProdFactory
    {
        [NotNull] private readonly IServices<IProdModel> mServices;
        [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;

        public ProdFactory([NotNull] IServices<IProdModel> services, [NotNull] ICodeLocationUtil codeLocationUtil)
        {
            if (services == null) throw new ArgumentNullException("services");
            if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");
            mServices = services;
            mCodeLocationUtil = codeLocationUtil;
        }

        public IProd Create(string defName, IBook book)
        {
            return new Prod(defName, book, mServices, mCodeLocationUtil);
        }
    }
}