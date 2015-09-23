﻿using System;
using System.Collections.Generic;
using NCase.All;
using NCase.Back.Api.Core.Parse;
using NCase.Front.Api;
using NDsl.Api.Dev.Core;
using NDsl.Api.Dev.Core.Nod;
using NDsl.Api.Dev.Core.Tok;
using NDsl.Api.Dev.Core.Util;
using NVisitor.Common.Quality;

namespace NCase.Back.Imp.Core.Parse
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