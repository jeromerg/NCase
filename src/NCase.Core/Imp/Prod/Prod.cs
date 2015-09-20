using NCase.Api;
using NCase.Api.Dev.Core.Parse;
using NCase.Imp.Core;
using NDsl.Api.Dev.Core;
using NDsl.Api.Dev.Core.Util;
using NVisitor.Common.Quality;

namespace NCase.Imp.Prod
{
    public class Prod : BlockDefBase<IProd>, IProd
    {
        public Prod(
            [NotNull] IParserGenerator parserGenerator,
            [NotNull] ITokenReaderWriter tokenReaderWriter,
            [NotNull] string caseSetName,
            [NotNull] ICodeLocationUtil codeLocationUtil)
            : base(parserGenerator, tokenReaderWriter, caseSetName, codeLocationUtil)
        {
        }

        protected override IProd GetDef()
        {
            return this;
        }
    }
}