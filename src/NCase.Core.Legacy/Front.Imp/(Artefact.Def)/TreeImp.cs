using JetBrains.Annotations;
using NCaseFramework.Back.Api.Tree;
using NCaseFramework.Front.Api.Tree;
using NCaseFramework.Front.Ui;
using NDsl.Back.Api.Record;
using NDsl.Back.Api.Util;
using NDsl.Front.Api;
using NDsl.Front.Imp;
using NDsl.Front.Ui;

namespace NCaseFramework.Front.Imp
{
    public class TreeImp : SetDefBaseImp<ITreeModel, TreeId, Definer>, Tree, ITreeModel
    {
        public class Factory : IDefFactory<Tree>
        {
            [NotNull] private readonly IServiceSet<ITreeModel> mServices;
            [NotNull] private readonly ICodeLocationFactory mCodeLocationFactory;
            [NotNull] private readonly ICodeLocationPrinter mCodeLocationPrinter;

            public Factory([NotNull] IServiceSet<ITreeModel> services, [NotNull] ICodeLocationFactory codeLocationFactory, [NotNull] ICodeLocationPrinter codeLocationPrinter)
            {
                mServices = services;
                mCodeLocationFactory = codeLocationFactory;
                mCodeLocationPrinter = codeLocationPrinter;
            }

            [NotNull]
            public Tree Create([NotNull] string defName, [NotNull] ITokenStream tokenStream)
            {
                return new TreeImp(defName, tokenStream, mServices, mCodeLocationFactory, mCodeLocationPrinter);
            }
        }

        public TreeImp([NotNull] string defName,
                       [NotNull] ITokenStream tokenStream,
                       [NotNull] IServiceSet<ITreeModel> services,
                       [NotNull] ICodeLocationFactory codeLocationFactory, 
                       [NotNull] ICodeLocationPrinter codeLocationPrinter)
            : base(new TreeId(defName), tokenStream, services, codeLocationFactory, codeLocationPrinter)
        {
        }

        public override ITreeModel Model
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