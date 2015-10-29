using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.Parse;
using NCaseFramework.Back.Api.SetDef;
using NCaseFramework.Front.Api.CaseEnumerable;
using NCaseFramework.Front.Api.SetDef;
using NCaseFramework.Front.Ui;
using NDsl.Back.Api.Common;

namespace NCaseFramework.Front.Imp
{
    public class GetCasesImp : IGetCases
    {
        [NotNull] private readonly IParserGenerator mParserGenerator;
        [NotNull] private readonly ICaseEnumerableFactory mCaseEnumerableFactory;

        public GetCasesImp([NotNull] IParserGenerator parserGenerator,
                           [NotNull] ICaseEnumerableFactory caseEnumerableFactory)
        {
            if (parserGenerator == null) throw new ArgumentNullException("parserGenerator");
            if (caseEnumerableFactory == null) throw new ArgumentNullException("caseEnumerableFactory");
            mParserGenerator = parserGenerator;
            mCaseEnumerableFactory = caseEnumerableFactory;
        }

        public CaseEnumerable Perform(ISetDefModel<ISetDefId> setDefModel)
        {
            IEnumerable<List<INode>> cases = Getcases(setDefModel);
            return mCaseEnumerableFactory.Create(cases);
        }

        public IEnumerable<List<INode>> Getcases(ISetDefModel<ISetDefId> setDefModel)
        {
            INode caseSetNode = mParserGenerator.Parse(setDefModel.Id, setDefModel.TokenStream);

            IEnumerable<List<INode>> cases = mParserGenerator.Generate(caseSetNode, new GenerateOptions(true));

            foreach (List<INode> cas in cases)
                yield return cas;
        }
    }
}