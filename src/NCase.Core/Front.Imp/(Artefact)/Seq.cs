using System;
using JetBrains.Annotations;
using NCase.Back.Api.Seq;
using NCase.Front.Api;
using NCase.Front.Ui;
using NDsl.All;
using NDsl.Back.Api.Core;

namespace NCase.Front.Imp
{
    public class Seq : SetDef<SeqId, ISeqApi>, ISeq, ISeqApi
    {

                public class Factory : ISeqFactory
        {
            [NotNull] private readonly IToolBox<ISeqApi> mToolBox;
            [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;

            public Factory([NotNull] IToolBox<ISeqApi> toolBox, [NotNull] ICodeLocationUtil codeLocationUtil)
            {
                if (toolBox == null) throw new ArgumentNullException("toolBox");
                if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");
                mToolBox = toolBox;
                mCodeLocationUtil = codeLocationUtil;
            }

            public ISeq Create(string defName, IBook book)
            {
                return new Seq(defName, book, mToolBox, mCodeLocationUtil);
            }
        }


        public Seq([NotNull] string defName, [NotNull] IBook book, [NotNull] IToolBox<ISeqApi> toolBox, [NotNull] ICodeLocationUtil codeLocationUtil)
            : base(new SeqId(defName), book, toolBox, codeLocationUtil)
        {
        }

        protected override ISeqApi GetApi()
        {
            return this;
        }
    }
}