using NCase.Api;
using NCase.Api.Dev.Core.Parse;
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
            [NotNull] ICodeLocationUtil codeLocationUtil)
            : base(parserGenerator, tokenReaderWriter, defName, codeLocationUtil)
        {
        }

        protected override IPairwise GetDef()
        {
            return this;
        }
    }
}