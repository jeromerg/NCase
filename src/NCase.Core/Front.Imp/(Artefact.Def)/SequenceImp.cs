using JetBrains.Annotations;
using NCaseFramework.Back.Api.Seq;
using NCaseFramework.Front.Api.Seq;
using NCaseFramework.Front.Ui;
using NDsl.Back.Api.Record;
using NDsl.Back.Api.Util;
using NDsl.Front.Api;
using NDsl.Front.Imp;
using NDsl.Front.Ui;

namespace NCaseFramework.Front.Imp
{
    public class SequenceImp : SetDefBaseImp<ISequenceModel, SequenceId, Definer>, Sequence, ISequenceModel
    {
        public class Factory : IDefFactory<Sequence>
        {
            [NotNull] private readonly IServiceSet<ISequenceModel> mServices;
            [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;

            public Factory([NotNull] IServiceSet<ISequenceModel> services, [NotNull] ICodeLocationUtil codeLocationUtil)
            {
                mServices = services;
                mCodeLocationUtil = codeLocationUtil;
            }

            [NotNull]
            public Sequence Create([NotNull] string defName, [NotNull] ITokenStream tokenStream)
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

        [NotNull] public override ISequenceModel Model
        {
            get { return this; }
        }

        [NotNull]
        public override Definer Define()
        {
            return new DefinerImp(Begin, End);
        }
    }
}