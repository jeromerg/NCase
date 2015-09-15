using System;
using NCase.Api;
using NCase.Api.Dev.Core.CaseSet;
using NDsl.Api.Dev.Core;
using NDsl.Api.Dev.Core.Util;
using NVisitor.Common.Quality;

namespace NCase.Imp.Pairwise
{
    public class PairwiseCaseSetFactory : ICaseSetFactory<IPairwise>
    {

        [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;

        public PairwiseCaseSetFactory([NotNull] ICodeLocationUtil codeLocationUtil)
        {
            if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");
            mCodeLocationUtil = codeLocationUtil;
        }

        public IPairwise Create([NotNull] ITokenWriter tokenWriter, [NotNull] string name)
        {
            if (tokenWriter == null) throw new ArgumentNullException("tokenWriter");
            if (name == null) throw new ArgumentNullException("name");
            return new PairwiseCaseSet(tokenWriter, name, mCodeLocationUtil);
        }
    }
}