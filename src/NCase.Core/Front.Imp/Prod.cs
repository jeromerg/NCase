﻿using System;
using JetBrains.Annotations;
using NCase.Back.Api.Prod;
using NCase.Front.Api;
using NDsl.Api.Core;


namespace NCase.Front.Imp
{
    public class Prod : DefBase<ProdId, IProd>, IProd
    {
        #region inner types

        public class Factory : IDefFactory<IProd>
        {
            private readonly IDefHelperFactory mDefHelperFactory;

            public Factory([NotNull] IDefHelperFactory defHelperFactory)
            {
                if (defHelperFactory == null) throw new ArgumentNullException("defHelperFactory");
                mDefHelperFactory = defHelperFactory;
            }

            public IProd Create([NotNull] ITokenReaderWriter tokenReaderWriter, [NotNull] string name)
            {
                return new Prod(tokenReaderWriter, name, mDefHelperFactory);
            }
        }

        #endregion

        public Prod([NotNull] ITokenReaderWriter tokenReaderWriter,
                    [NotNull] string defName,
                    [NotNull] IDefHelperFactory defHelperFactory)
            : base(new ProdId(), defName, tokenReaderWriter, defHelperFactory)
        {
        }
    }
}