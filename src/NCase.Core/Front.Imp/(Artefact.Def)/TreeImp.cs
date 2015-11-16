using System;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.Tree;
using NCaseFramework.Front.Api.Tree;
using NCaseFramework.Front.Ui;
using NDsl.Back.Api.TokenStream;
using NDsl.Back.Api.Util;
using NDsl.Front.Api;

namespace NCaseFramework.Front.Imp
{
    public class TreeImp : SetDefBaseImp<ITreeModel, TreeId>, Tree, ITreeModel
    {
        public class Factory : IDefFactory<Tree>
        {
            [NotNull] private readonly IServiceSet<ITreeModel> mServices;
            [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;

            public Factory([NotNull] IServiceSet<ITreeModel> services, [NotNull] ICodeLocationUtil codeLocationUtil)
            {
                if (services == null) throw new ArgumentNullException("services");
                if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");
                mServices = services;
                mCodeLocationUtil = codeLocationUtil;
            }

            public Tree Create(string defName, ITokenStream tokenStream)
            {
                return new TreeImp(defName, tokenStream, mServices, mCodeLocationUtil);
            }
        }

        public TreeImp([NotNull] string defName,
                       [NotNull] ITokenStream tokenStream,
                       [NotNull] IServiceSet<ITreeModel> services,
                       [NotNull] ICodeLocationUtil codeLocationUtil)
            : base(new TreeId(defName), tokenStream, services, codeLocationUtil)
        {
        }

        public override ITreeModel Model
        {
            get { return this; }
        }
    }
}