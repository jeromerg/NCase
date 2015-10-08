using System;
using JetBrains.Annotations;
using NCase.Back.Api.Tree;
using NCase.Front.Api;
using NCase.Front.Ui;
using NDsl.All;
using NDsl.Back.Api.Core;

namespace NCase.Front.Imp
{
    public class Tree : SetDef<ITreeApi, TreeId>, ITree, ITreeApi
    {
        public class Factory : ITreeFactory
        {
            [NotNull] private readonly IToolBox<ITreeApi> mToolBox;
            [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;

            public Factory([NotNull] IToolBox<ITreeApi> toolBox, [NotNull] ICodeLocationUtil codeLocationUtil)
            {
                if (toolBox == null) throw new ArgumentNullException("toolBox");
                if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");
                mToolBox = toolBox;
                mCodeLocationUtil = codeLocationUtil;
            }

            public ITree Create(string defName, IBook book)
            {
                return new Tree(defName, book, mToolBox, mCodeLocationUtil);
            }
        }

        public Tree([NotNull] string defName,
                    [NotNull] IBook book,
                    [NotNull] IToolBox<ITreeApi> toolBox,
                    [NotNull] ICodeLocationUtil codeLocationUtil)
            : base(new TreeId(defName), book, toolBox, codeLocationUtil)
        {
        }

        public override ITreeApi Api
        {
            get { return this; }
        }
    }
}