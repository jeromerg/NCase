using System;
using NCase.Api.Dev.Core;
using NCase.Api.Pub;
using NCase.Imp.Core;
using NDsl.Api.Dev.Core;
using NVisitor.Common.Quality;

namespace NCase.Imp.Prod
{
    public class Prod : DefBase<IProd>, IProd
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
            : base(defName, tokenReaderWriter, defHelperFactory)
        {
        }


        protected override IProd GetDef()
        {
            return this;
        }
    }
}