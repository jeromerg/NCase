using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCase.Back.Api.Parse;
using NDsl.Back.Api.Core;

namespace NCase.Back.Imp.Parse
{
    public class ParserGenerator : IParserGenerator
    {
        [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;
        [NotNull] private readonly IGenerateDirector mCaseGenerator;
        [NotNull] private readonly IParseDirector mParseDirector;

        public ParserGenerator([NotNull] ICodeLocationUtil codeLocationUtil,
                               [NotNull] IGenerateDirector caseGenerator,
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
            return mParseDirector.GetReferencedNode<INode>(def, mCodeLocationUtil.GetCurrentUserCodeLocation());
        }

        public IEnumerable<List<INode>> Generate(INode caseSetNode)
        {
            IEnumerable<List<INode>> factForEachTestCase = mCaseGenerator.Visit(caseSetNode);
            foreach (List<INode> testCaseNodes in factForEachTestCase)
                yield return testCaseNodes;
        }
    }
}