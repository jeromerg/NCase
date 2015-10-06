using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCase.Back.Api.Parse;
using NCase.Front.Imp.Artefact;
using NCase.Front.Ui;
using NDsl.Back.Api.Core;
using NDsl.Front.Api;

namespace NCase.Front.Imp.Op
{
    public class GetCasesImpl : IOperationImp<ISetDef, GetCases, ISetDefImp, ICaseEnumerable>
    {
        [NotNull] private readonly IParserGenerator mParserGenerator;
        [NotNull] private readonly CaseEnumerableImp.Factory mCaseEnumerableFactory;

        public GetCasesImpl([NotNull] IParserGenerator parserGenerator,
                            [NotNull] CaseEnumerableImp.Factory caseEnumerableFactory)
        {
            if (parserGenerator == null) throw new ArgumentNullException("parserGenerator");
            if (caseEnumerableFactory == null) throw new ArgumentNullException("caseEnumerableFactory");
            mParserGenerator = parserGenerator;
            mCaseEnumerableFactory = caseEnumerableFactory;
        }

        public ICaseEnumerable Perform(IOperationDirector director, GetCases uiGetCases, ISetDefImp setDefImp)
        {
            IEnumerable<List<INode>> cases = Getcases(setDefImp, uiGetCases);
            return mCaseEnumerableFactory.Create(cases);
        }

        public IEnumerable<List<INode>> Getcases(ISetDefImp setDefImp, GetCases uiGetCases)
        {
            INode caseSetNode = mParserGenerator.Parse(setDefImp.DefId, setDefImp.TokenReaderWriter);

            IEnumerable<List<INode>> cases = mParserGenerator.Generate(caseSetNode, new GenerateOptions(true));

            foreach (List<INode> cas in cases)
                yield return cas;
        }
    }
}