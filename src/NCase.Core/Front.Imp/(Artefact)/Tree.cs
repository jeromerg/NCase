using System;
using JetBrains.Annotations;
using NCase.Back.Api.Tree;
using NCase.Front.Api;
using NCase.Front.Ui;
using NDsl.All;
using NDsl.Back.Api.Core;

namespace NCase.Front.Imp
{
    public class Tree : SetDef<ITreeModel, TreeId>, ITree, ITreeModel
    {
        public class Factory : ITreeFactory
        {
            [NotNull] private readonly IServices<ITreeModel> mServices;
            [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;

            public Factory([NotNull] IServices<ITreeModel> services, [NotNull] ICodeLocationUtil codeLocationUtil)
            {
                if (services == null) throw new ArgumentNullException("services");
                if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");
                mServices = services;
                mCodeLocationUtil = codeLocationUtil;
            }

            public ITree Create(string defName, IBook book)
            {
                return new Tree(defName, book, mServices, mCodeLocationUtil);
            }
        }

        public Tree([NotNull] string defName,
                    [NotNull] IBook book,
                    [NotNull] IServices<ITreeModel> services,
                    [NotNull] ICodeLocationUtil codeLocationUtil)
            : base(new TreeId(defName), book, services, codeLocationUtil)
        {
        }

        public override ITreeModel Model
        {
            get { return this; }
        }
    }
}