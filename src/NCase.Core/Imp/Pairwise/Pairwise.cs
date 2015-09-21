using System;
using NCase.Api.Dev.Core;
using NCase.Api.Pub;
using NCase.Imp.Core;
using NDsl.Api.Dev.Core;
using NVisitor.Common.Quality;

namespace NCase.Imp.Pairwise
{
    public class Pairwise : DefBase<IPairwise>, IPairwise
    {
        #region inner types

        public class Factory : IDefFactory<IPairwise>
        {
            private readonly IDefHelperFactory mDefHelperFactory;

            public Factory([NotNull] IDefHelperFactory defHelperFactory)
            {
                if (defHelperFactory == null) throw new ArgumentNullException("defHelperFactory");
                mDefHelperFactory = defHelperFactory;
            }

            public IPairwise Create([NotNull] ITokenReaderWriter tokenReaderWriter, [NotNull] string name)
            {
                return new Pairwise(tokenReaderWriter, name, mDefHelperFactory);
            }
        }

        #endregion

        public Pairwise([NotNull] ITokenReaderWriter tokenReaderWriter,
                        [NotNull] string defName,
                        [NotNull] IDefHelperFactory defHelperFactory)
            : base(defName, tokenReaderWriter, defHelperFactory)
        {
        }


        protected override IPairwise GetDef()
        {
            return this;
        }
    }
}