using System;
using JetBrains.Annotations;
using NCase.Back.Api.Seq;
using NCase.Front.Api;
using NCase.Front.Ui;
using NDsl.All;
using NDsl.Back.Api.Core;

namespace NCase.Front.Imp
{
    public class Seq : SetDef<ISeqModel, SeqId>, ISeq, ISeqModel
    {

                public class Factory : ISeqFactory
        {
            [NotNull] private readonly IServices<ISeqModel> mServices;
            [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;

            public Factory([NotNull] IServices<ISeqModel> services, [NotNull] ICodeLocationUtil codeLocationUtil)
            {
                if (services == null) throw new ArgumentNullException("services");
                if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");
                mServices = services;
                mCodeLocationUtil = codeLocationUtil;
            }

            public ISeq Create(string defName, IBook book)
            {
                return new Seq(defName, book, mServices, mCodeLocationUtil);
            }
        }


        public Seq([NotNull] string defName, [NotNull] IBook book, [NotNull] IServices<ISeqModel> services, [NotNull] ICodeLocationUtil codeLocationUtil)
            : base(new SeqId(defName), book, services, codeLocationUtil)
        {
        }

        public override ISeqModel Model
        {
            get { return this; }
        }
    }
}