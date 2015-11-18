using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.Parse;
using NDsl.All.Def;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Record;
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
            mCodeLocationUtil = codeLocationUtil;
            mCaseGenerator = caseGenerator;
            mParseDirector = parseDirector;
        }

        public INode Parse([NotNull] IDefId def, [NotNull] ITokenReader tokenReader)
        {
            if (def == null) throw new ArgumentNullException("def");
            if (tokenReader == null) throw new ArgumentNullException("tokenReader");

            // PARSE
            foreach (IToken token in tokenReader.Tokens)
                mParseDirector.Visit(token);

            // GENERATE CASES
            return mParseDirector.GetNodeForId<INode>(def, mCodeLocationUtil.GetCurrentUserCodeLocation());
        }

        [NotNull, ItemNotNull]
        public IEnumerable<List<INode>> Generate([NotNull] INode caseSetNode, [NotNull] GenerateOptions options)
        {
            if (caseSetNode == null) throw new ArgumentNullException("caseSetNode");
            if (options == null) throw new ArgumentNullException("options");

            IEnumerable<List<INode>> factForEachTestCase = mCaseGenerator.Visit(caseSetNode, options);
            foreach (List<INode> testCaseNodes in factForEachTestCase)
                yield return testCaseNodes;
        }
    }
}