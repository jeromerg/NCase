using NCase.Api;
using NCase.Api.Dev.Core;
using NCase.Api.Dev.Core.Parse;
using NCase.Api.Pub;
using NCase.Imp.Core;
using NDsl.Api.Dev.Core;
using NDsl.Api.Dev.Core.Util;
using NVisitor.Common.Quality;

namespace NCase.Imp.Pairwise
{
    public class Pairwise : BlockDefBase<IPairwise>, IPairwise
    {
        public Pairwise(
            [NotNull] IParserGenerator parserGenerator,
            [NotNull] ITokenReaderWriter tokenReaderWriter,
            [NotNull] string defName,
            [NotNull] ICodeLocationUtil codeLocationUtil,
            [NotNull] ISetFactory setFactory)
            : base(parserGenerator, tokenReaderWriter, defName, codeLocationUtil, setFactory)
        {
        }

        protected override IPairwise GetDef()
        {
            return this;
        }
    }
}