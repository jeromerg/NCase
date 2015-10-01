using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCase.Back.Api.Parse;
using NCase.Front.Ui;
using NDsl.Back.Api.Core;
using NDsl.Front.Api;

namespace NCase.Front.Imp
{
    public class GetCasesImpl : IOperationImp<ISetDef, GetCases, ISetDefImp, IEnumerable<ICase>>
    {
        [NotNull] private readonly IParserGenerator mParserGenerator;
        [NotNull] private readonly ICaseFactory mCaseFactory;

        public GetCasesImpl([NotNull] IParserGenerator parserGenerator, [NotNull] ICaseFactory caseFactory)
        {
            if (parserGenerator == null) throw new ArgumentNullException("parserGenerator");
            if (caseFactory == null) throw new ArgumentNullException("caseFactory");
            mParserGenerator = parserGenerator;
            mCaseFactory = caseFactory;
        }

        public IEnumerable<ICase> Perform(IOperationDirector director, GetCases operation, ISetDefImp setDefImp)
        {
            INode caseSetNode = mParserGenerator.Parse(setDefImp.DefId, setDefImp.TokenReaderWriter);

            IEnumerable<List<INode>> cases = mParserGenerator.Generate(caseSetNode);

            foreach (List<INode> cas in cases)
                yield return mCaseFactory.Create(cas);
        }
    }
}