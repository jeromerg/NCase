using System;
using JetBrains.Annotations;
using NCase.Back.Api.Prod;
using NCase.Front.Api;
using NCase.Front.Ui;
using NDsl.All;
using NDsl.Back.Api.Core;

namespace NCase.Front.Imp
{
    public class Prod : SetDef<IProdModel, ProdId>, IProd, IProdModel
    {
        public class Factory : IProdFactory
        {
            [NotNull] private readonly IServices<IProdModel> mServices;
            [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;

            public Factory([NotNull] IServices<IProdModel> services, [NotNull] ICodeLocationUtil codeLocationUtil)
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

        public Prod([NotNull] string defName,
                    [NotNull] IBook book,
                    [NotNull] IServices<IProdModel> services,
                    [NotNull] ICodeLocationUtil codeLocationUtil)
            : base(new ProdId(defName), book, services, codeLocationUtil)
        {
        }

        public override IProdModel Model
        {
            get { return this; }
        }
    }
}