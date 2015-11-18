using System;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.Print;
using NCaseFramework.Front.Api.Case;
using NCaseFramework.Front.Api.SetDef;
using NDsl.Back.Api.Common;

namespace NCaseFramework.Front.Imp
{
    public class PrintCaseSvc : IPrintCaseSvc
    {
        [NotNull] private readonly IPrintCaseDirector mPrintCaseDirector;

        public PrintCaseSvc([NotNull] IPrintCaseDirector printCaseDirector)
        {
            mPrintCaseDirector = printCaseDirector;
        }

        [NotNull]
        public string PrintCase([NotNull] ICaseModel caseModel)
        {
            if (caseModel == null) throw new ArgumentNullException("caseModel");

            IPrintCasePayload printCasePayload = mPrintCaseDirector.NewPayload();

            foreach (INode fact in caseModel.FactNodes)
                mPrintCaseDirector.Visit(fact, printCasePayload);

            return printCasePayload.GetString();
        }
    }
}