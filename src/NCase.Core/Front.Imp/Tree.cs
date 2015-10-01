﻿using System;
using JetBrains.Annotations;
using NCase.Back.Api.Tree;
using NCase.Front.Ui;
using NDsl.Back.Api.Core;
using NDsl.Front.Api;

namespace NCase.Front.Imp
{
    public class Tree : SetDefImpBase<ITree, TreeId, Tree>, ITree, IDefImp<TreeId>
    {
        #region inner types

        public class Factory : IDefFactory<ITree>
        {
            private readonly ICodeLocationUtil mCodeLocationUtil;
            private readonly IOperationDirector mOperationDirector;

            public Factory([NotNull] ICodeLocationUtil codeLocationUtil, [NotNull] IOperationDirector operationDirector)
            {
                if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");
                if (operationDirector == null) throw new ArgumentNullException("operationDirector");
                mCodeLocationUtil = codeLocationUtil;
                mOperationDirector = operationDirector;
            }

            public ITree Create([NotNull] ITokenReaderWriter tokenReaderWriter, [NotNull] string name)
            {
                return new Tree(name, tokenReaderWriter, mCodeLocationUtil, mOperationDirector);
            }
        }

        #endregion

        [NotNull] private readonly TreeId mId;

        public Tree([NotNull] string defName,
                    [NotNull] ITokenReaderWriter tokenReaderWriter,
                    [NotNull] ICodeLocationUtil codeLocationUtil,
                    [NotNull] IOperationDirector operationDirector)
            : base(tokenReaderWriter, codeLocationUtil, operationDirector)
        {
            mId = new TreeId(defName);
        }

        protected override Tree ThisDefImpl
        {
            get { return this; }
        }

        public TreeId Id
        {
            get { return mId; }
        }
    }
}