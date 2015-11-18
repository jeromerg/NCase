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
    public class GetCasesSvc : IGetCasesSvc
    {
        [NotNull] private readonly IParserGenerator mParserGenerator;
        [NotNull] private readonly ICaseFactory mCaseFactory;

        public GetCasesSvc([NotNull] IParserGenerator parserGenerator, [NotNull] ICaseFactory caseFactory)
        {
            mParserGenerator = parserGenerator;
            mCaseFactory = caseFactory;
        }

        [NotNull] 
        public IEnumerable<Case> GetCases([NotNull] ISetDefModel<ISetDefId> setDefModel)
        {
            if (setDefModel == null) throw new ArgumentNullException("setDefModel");

            IEnumerable<List<INode>> cases = GetcasesImp(setDefModel);
            foreach (List<INode> @case in cases)
                yield return mCaseFactory.Create(@case, setDefModel.TokenStream);
        }

        [NotNull, ItemNotNull] 
        private IEnumerable<List<INode>> GetcasesImp([NotNull] ISetDefModel<ISetDefId> setDefModel)
        {
            using (setDefModel.TokenStream.SetReadMode())
            {
                INode caseSetNode = mParserGenerator.Parse(setDefModel.Id, setDefModel.TokenStream);

                IEnumerable<List<INode>> cases = mParserGenerator.Generate(caseSetNode, new GenerateOptions(true));

                foreach (List<INode> cas in cases)
                    yield return cas;
            }
        }
    }
}