using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCase.Back.Api.Parse;
using NCase.Front.Api;
using NCase.Front.Ui;
using NDsl.All;
using NDsl.Back.Api.Core;

namespace NCase.Front.Imp.Op
{
    public class GetCases : ITool<ISetDefApi>, IGetCases
    {
        [NotNull] private readonly IParserGenerator mParserGenerator;
        [NotNull] private readonly CaseEnumerable.Factory mCaseEnumerableFactory;

        public GetCases([NotNull] IParserGenerator parserGenerator,
                            [NotNull] CaseEnumerable.Factory caseEnumerableFactory)
        {
            if (parserGenerator == null) throw new ArgumentNullException("parserGenerator");
            if (caseEnumerableFactory == null) throw new ArgumentNullException("caseEnumerableFactory");
            mParserGenerator = parserGenerator;
            mCaseEnumerableFactory = caseEnumerableFactory;
        }

        public ICaseEnumerable Perform(ISetDefApi setDefApi)
        {
            IEnumerable<List<INode>> cases = Getcases(setDefApi);
            return mCaseEnumerableFactory.Create(cases);
        }

        public IEnumerable<List<INode>> Getcases(ISetDefApi setDefApi)
        {
            INode caseSetNode = mParserGenerator.Parse(setDefApi.Id, setDefApi.Book);

            IEnumerable<List<INode>> cases = mParserGenerator.Generate(caseSetNode, new GenerateOptions(true));

            foreach (List<INode> cas in cases)
                yield return cas;
        }
    }
}