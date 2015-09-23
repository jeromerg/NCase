using System;
using NCase.Api.Dev.Core;
using NCase.Api.Pub;
using NCase.Imp.Core;
using NDsl.Api.Dev.Core;
using NVisitor.Common.Quality;

namespace NCase.Imp.Tree
{
    public class Tree : DefBase<ITree>, ITree
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
            : base(defName, tokenReaderWriter, defHelperFactory)
        {
        }


        protected override ITree GetDef()
        {
            return this;
        }
    }
}