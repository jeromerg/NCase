using NCase.Api.Dev.Core;
using NCase.Api.Dev.Core.Parse;
using NCase.Api.Pub;
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
            [NotNull] ICodeLocationUtil codeLocationUtil,
            [NotNull] ISetFactory setFactory)
            : base(parserGenerator, tokenReaderWriter, caseSetName, codeLocationUtil, setFactory)
        {
        }

        protected override IProd GetDef()
        {
            return this;
        }
    }
}