using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCase.Back.Api.Parse;
using NCase.Front.Ui;
using NDsl.Back.Api.Core;
using NDsl.Front.Api;

namespace NCase.Front.Imp
{
    public class GetCasesImpl : IOperationImp<ISetDef, GetCases, ISetDefImp, ICaseEnumerable>
    {
        [NotNull] private readonly IParserGenerator mParserGenerator;
        [NotNull] private readonly ICaseEnumerableFactory mCaseEnumerableFactory;

        public GetCasesImpl([NotNull] IParserGenerator parserGenerator,
                            [NotNull] ICaseEnumerableFactory caseEnumerableFactory)
        {
            if (parserGenerator == null) throw new ArgumentNullException("parserGenerator");
            if (caseEnumerableFactory == null) throw new ArgumentNullException("caseEnumerableFactory");
            mParserGenerator = parserGenerator;
            mCaseEnumerableFactory = caseEnumerableFactory;
        }

        public ICaseEnumerable Perform(IOperationDirector director, GetCases operation, ISetDefImp setDefImp)
        {
            IEnumerable<IEnumerable<INode>> cases = Getcases(setDefImp);
            return mCaseEnumerableFactory.Create(cases);
        }

        public IEnumerable<IEnumerable<INode>> Getcases(ISetDefImp setDefImp)
        {
            INode caseSetNode = mParserGenerator.Parse(setDefImp.DefId, setDefImp.TokenReaderWriter);

            IEnumerable<List<INode>> cases = mParserGenerator.Generate(caseSetNode);

            foreach (List<INode> cas in cases)
                yield return cas;
        }
    }
}