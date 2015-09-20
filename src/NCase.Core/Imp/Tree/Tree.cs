using NCase.Api;
using NCase.Api.Dev.Core.Parse;
using NCase.Imp.Core;
using NDsl.Api.Dev.Core;
using NDsl.Api.Dev.Core.Util;
using NVisitor.Common.Quality;

namespace NCase.Imp.Tree
{
    public class Tree : BlockDefBase<ITree>, ITree
    {
        public Tree(
            [NotNull] IParserGenerator parserGenerator,
            [NotNull] ITokenReaderWriter tokenReaderWriter, 
            [NotNull] string defName,
            [NotNull] ICodeLocationUtil codeLocationUtil) 
            : base(parserGenerator, tokenReaderWriter, defName, codeLocationUtil)
        {
        }

        protected override ITree GetDef()
        {
            return this;
        }
    }
}