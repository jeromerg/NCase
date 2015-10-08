using System;
using JetBrains.Annotations;
using NCase.Back.Api.Prod;
using NCase.Front.Api;
using NCase.Front.Ui;
using NDsl.All;
using NDsl.Back.Api.Core;

namespace NCase.Front.Imp
{
    public class Prod : SetDef<IProdApi, ProdId>, IProd, IProdApi
    {
        public class Factory : IProdFactory
        {
            [NotNull] private readonly IToolBox<IProdApi> mToolBox;
            [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;

            public Factory([NotNull] IToolBox<IProdApi> toolBox, [NotNull] ICodeLocationUtil codeLocationUtil)
            {
                if (toolBox == null) throw new ArgumentNullException("toolBox");
                if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");
                mToolBox = toolBox;
                mCodeLocationUtil = codeLocationUtil;
            }

            public IProd Create(string defName, IBook book)
            {
                return new Prod(defName, book, mToolBox, mCodeLocationUtil);
            }
        }

        public Prod([NotNull] string defName,
                    [NotNull] IBook book,
                    [NotNull] IToolBox<IProdApi> toolBox,
                    [NotNull] ICodeLocationUtil codeLocationUtil)
            : base(new ProdId(defName), book, toolBox, codeLocationUtil)
        {
        }

        public override IProdApi Api
        {
            get { return this; }
        }
    }
}