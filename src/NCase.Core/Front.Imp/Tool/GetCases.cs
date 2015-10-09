using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCase.Back.Api.Parse;
using NCase.Back.Api.SetDef;
using NCase.Front.Api.SetDef;
using NCase.Front.Imp.CaseEnumerable;
using NCase.Front.Ui;
using NDsl.Back.Api.Common;

namespace NCase.Front.Imp.Tool
{
    public class GetCases : IGetCases
    {
        [NotNull] private readonly IParserGenerator mParserGenerator;
        [NotNull] private readonly CaseEnumerablefactory mCaseEnumerableFactory;

        public GetCases([NotNull] IParserGenerator parserGenerator,
                        [NotNull] CaseEnumerablefactory caseEnumerableFactory)
        {
            if (parserGenerator == null) throw new ArgumentNullException("parserGenerator");
            if (caseEnumerableFactory == null) throw new ArgumentNullException("caseEnumerableFactory");
            mParserGenerator = parserGenerator;
            mCaseEnumerableFactory = caseEnumerableFactory;
        }

        public ICaseEnumerable Perform(ISetDefModel<ISetDefId> setDefModel)
        {
            IEnumerable<List<INode>> cases = Getcases(setDefModel);
            return mCaseEnumerableFactory.Create(cases);
        }

        public IEnumerable<List<INode>> Getcases(ISetDefModel<ISetDefId> setDefModel)
        {
            INode caseSetNode = mParserGenerator.Parse(setDefModel.Id, setDefModel.Book);

            IEnumerable<List<INode>> cases = mParserGenerator.Generate(caseSetNode, new GenerateOptions(true));

            foreach (List<INode> cas in cases)
                yield return cas;
        }
    }
}