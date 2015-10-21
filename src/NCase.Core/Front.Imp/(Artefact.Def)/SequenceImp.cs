using System;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.Seq;
using NCaseFramework.Front.Api.Seq;
using NCaseFramework.Front.Ui;
using NDsl.Back.Api.Book;
using NDsl.Back.Api.Util;
using NDsl.Front.Api;

namespace NCaseFramework.Front.Imp
{
    public class SequenceImp : SetDefBaseImp<ISequenceModel, SeqId>, Sequence, ISequenceModel
    {
        public class Factory : IDefFactory<Sequence>
        {
            [NotNull] private readonly IServices<ISequenceModel> mServices;
            [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;

            public Factory([NotNull] IServices<ISequenceModel> services, [NotNull] ICodeLocationUtil codeLocationUtil)
            {
                if (services == null) throw new ArgumentNullException("services");
                if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");
                mServices = services;
                mCodeLocationUtil = codeLocationUtil;
            }

            public Sequence Create(string defName, ITokenStream tokenStream)
            {
                return new SequenceImp(defName, tokenStream, mServices, mCodeLocationUtil);
            }
        }

        public SequenceImp([NotNull] string defName,
                           [NotNull] ITokenStream tokenStream,
                           [NotNull] IServices<ISequenceModel> services,
                           [NotNull] ICodeLocationUtil codeLocationUtil)
            : base(new SeqId(defName), tokenStream, services, codeLocationUtil)
        {
        }

        public override ISequenceModel Model
        {
            get { return this; }
        }
    }
}