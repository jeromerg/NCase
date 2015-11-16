using System;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.Seq;
using NCaseFramework.Front.Api.Seq;
using NCaseFramework.Front.Ui;
using NDsl.Back.Api.TokenStream;
using NDsl.Back.Api.Util;
using NDsl.Front.Api;

namespace NCaseFramework.Front.Imp
{
    public class SequenceImp : SetDefBaseImp<ISequenceModel, SequenceId>, Sequence, ISequenceModel
    {
        public class Factory : IDefFactory<Sequence>
        {
            [NotNull] private readonly IServiceSet<ISequenceModel> mServices;
            [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;

            public Factory([NotNull] IServiceSet<ISequenceModel> services, [NotNull] ICodeLocationUtil codeLocationUtil)
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
                           [NotNull] IServiceSet<ISequenceModel> services,
                           [NotNull] ICodeLocationUtil codeLocationUtil)
            : base(new SequenceId(defName), tokenStream, services, codeLocationUtil)
        {
        }

        public override ISequenceModel Model
        {
            get { return this; }
        }
    }
}