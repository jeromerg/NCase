using System;
using JetBrains.Annotations;
using NCase.Back.Api.Tree;
using NCase.Front.Api.Tree;
using NCase.Front.Imp.SetDef;
using NCase.Front.Ui;
using NDsl.Back.Api.Book;
using NDsl.Back.Api.Util;

namespace NCase.Front.Imp.Tree
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

            public ITree Create(string defName, ITokenStream tokenStream)
            {
                return new Tree(defName, tokenStream, mServices, mCodeLocationUtil);
            }
        }

        public Tree([NotNull] string defName,
                    [NotNull] ITokenStream tokenStream,
                    [NotNull] IServices<ITreeModel> services,
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