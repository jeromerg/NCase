using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCase.Back.Api.Core;
using NCase.Back.Api.Parse;
using NDsl.Api.Core;

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

        public IEnumerable<List<INode>> ParseAndGenerate(IDefId def, ITokenReader tokenReader)
        {
            // PARSE
            foreach (IToken token in tokenReader.Tokens)
                mParseDirector.Visit(token);

            // GENERATE CASES
            var reference = mParseDirector.GetReferencedNode<INode>(def, mCodeLocationUtil.GetCurrentUserCodeLocation());

            // WRAP RESULT INTO ICase
            foreach (List<INode> testCaseNodes in mCaseGenerator.Visit(reference))
                yield return testCaseNodes;
        }
    }
}