using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.Parse;
using NCaseFramework.Back.Api.SetDef;
using NCaseFramework.Front.Api.Case;
using NCaseFramework.Front.Api.SetDef;
using NCaseFramework.Front.Ui;
using NDsl.Back.Api.Common;

namespace NCaseFramework.Front.Imp
{
    public class GetCasesImp : IGetCases
    {
        [NotNull] private readonly IParserGenerator mParserGenerator;
        private readonly ICaseFactory mCaseFactory;

        public GetCasesImp([NotNull] IParserGenerator parserGenerator, [NotNull] ICaseFactory caseFactory)
        {
            if (parserGenerator == null) throw new ArgumentNullException("parserGenerator");
            if (caseFactory == null) throw new ArgumentNullException("caseFactory");
            mParserGenerator = parserGenerator;
            mCaseFactory = caseFactory;
        }

        public IEnumerable<Case> Perform(ISetDefModel<ISetDefId> setDefModel)
        {
            IEnumerable<List<INode>> cases = Getcases(setDefModel);
            foreach (List<INode> @case in cases)
                yield return mCaseFactory.Create(@case);
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