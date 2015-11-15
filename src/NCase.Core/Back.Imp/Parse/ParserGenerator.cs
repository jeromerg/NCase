using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.Parse;
using NDsl.All.Def;
using NDsl.Back.Api.Book;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Util;

namespace NCaseFramework.Back.Imp.Parse
{
    public class ParserGenerator : IParserGenerator
    {
        [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;
        [NotNull] private readonly IGenerateCasesDirector mCaseGenerator;
        [NotNull] private readonly IParseDirector mParseDirector;

        public ParserGenerator([NotNull] ICodeLocationUtil codeLocationUtil,
                               [NotNull] IGenerateCasesDirector caseGenerator,
                               [NotNull] IParseDirector parseDirector)
        {
            if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");
            if (caseGenerator == null) throw new ArgumentNullException("caseGenerator");
            if (parseDirector == null) throw new ArgumentNullException("parseDirector");
            mCodeLocationUtil = codeLocationUtil;
            mCaseGenerator = caseGenerator;
            mParseDirector = parseDirector;
        }

        public INode Parse(IDefId def, ITokenReader tokenReader)
        {
            // PARSE
            foreach (IToken token in tokenReader.Tokens)
                mParseDirector.Visit(token);

            // GENERATE CASES
            return mParseDirector.GetNodeForId<INode>(def, mCodeLocationUtil.GetCurrentUserCodeLocation());
        }

        public IEnumerable<List<INode>> Generate(INode caseSetNode, GenerateOptions options)
        {
            IEnumerable<List<INode>> factForEachTestCase = mCaseGenerator.Visit(caseSetNode, options);
            foreach (List<INode> testCaseNodes in factForEachTestCase)
                yield return testCaseNodes;
        }
    }
}