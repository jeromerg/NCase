﻿using System;
using NCase.Back.Api.Tree;
using NCase.Front.Api;
using NDsl.Back.Api.Core;
using NVisitor.Common.Quality;

namespace NCase.Front.Imp
{
    public class Tree : DefBase<TreeId>, ITree
    {
        #region inner types

        public class Factory : IDefFactory<ITree>
        {
            private readonly IDefHelperFactory mDefHelperFactory;

            public Factory([NotNull] IDefHelperFactory defHelperFactory)
            {
                if (defHelperFactory == null) throw new ArgumentNullException("defHelperFactory");
                mDefHelperFactory = defHelperFactory;
            }

            public ITree Create([NotNull] ITokenReaderWriter tokenReaderWriter, [NotNull] string name)
            {
                return new Tree(tokenReaderWriter, name, mDefHelperFactory);
            }
        }

        #endregion

        public Tree([NotNull] ITokenReaderWriter tokenReaderWriter,
                    [NotNull] string defName,
                    [NotNull] IDefHelperFactory defHelperFactory)
            : base(new TreeId(), defName, tokenReaderWriter, defHelperFactory)
        {
        }
    }
}