using System;
using NCase.Api;
using NCase.Api.Dev;
using NDsl.Api.Core;
using NDsl.Api.Core.Util;
using NVisitor.Common.Quality;

namespace NCase.Imp.Prod
{
    public class ProductCaseSetFactory : ICaseSetFactory<ICardinalProduct>
    {

        [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;

        public ProductCaseSetFactory([NotNull] ICodeLocationUtil codeLocationUtil)
        {
            if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");
            mCodeLocationUtil = codeLocationUtil;
        }

        public ICardinalProduct Create([NotNull] ITokenWriter tokenWriter, [NotNull] string name)
        {
            if (tokenWriter == null) throw new ArgumentNullException("tokenWriter");
            if (name == null) throw new ArgumentNullException("name");
            return new ProductCaseSet(tokenWriter, name, mCodeLocationUtil);
        }
    }
}