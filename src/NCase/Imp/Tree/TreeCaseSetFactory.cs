using System;
using NCase.Api;
using NCase.Api.Dev;
using NDsl.Api.Core;
using NDsl.Api.Core.Util;
using NVisitor.Common.Quality;

namespace NCase.Imp.Tree
{
    public class TreeCaseSetFactory : ICaseSetFactory<ITree>
    {

        [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;

        public TreeCaseSetFactory([NotNull] ICodeLocationUtil codeLocationUtil)
        {
            if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");
            mCodeLocationUtil = codeLocationUtil;
        }

        public ITree Create([NotNull] ITokenWriter tokenWriter, [NotNull] string name)
        {
            if (tokenWriter == null) throw new ArgumentNullException("tokenWriter");
            if (name == null) throw new ArgumentNullException("name");
            return new TreeCaseSet(tokenWriter, name, mCodeLocationUtil);
        }
    }
}