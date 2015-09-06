using System;
using NCase.Api;
using NCase.Api.Dev.Core.CaseSet;
using NDsl.Api.Core;
using NDsl.Api.Core.Util;
using NVisitor.Common.Quality;

namespace NCase.Imp.Prod
{
    public class ProdCaseSetFactory : ICaseSetFactory<IProd>
    {

        [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;

        public ProdCaseSetFactory([NotNull] ICodeLocationUtil codeLocationUtil)
        {
            if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");
            mCodeLocationUtil = codeLocationUtil;
        }

        public IProd Create([NotNull] ITokenWriter tokenWriter, [NotNull] string name)
        {
            if (tokenWriter == null) throw new ArgumentNullException("tokenWriter");
            if (name == null) throw new ArgumentNullException("name");
            return new ProdCaseSet(tokenWriter, name, mCodeLocationUtil);
        }
    }
}