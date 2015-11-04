using JetBrains.Annotations;
using NCaseFramework.Back.Api.Print;
using NCaseFramework.Front.Api.Case;
using NCaseFramework.Front.Api.SetDef;
using NDsl.Back.Api.Common;

namespace NCaseFramework.Front.Imp
{
    public class PrintCaseImp : IPrintCase
    {
        [NotNull] private readonly IPrintCaseDirector mPrintCaseDirector;

        public PrintCaseImp([NotNull] IPrintCaseDirector printCaseDirector)
        {
            mPrintCaseDirector = printCaseDirector;
        }

        public string Perform(ICaseModel caseModel)
        {
            IPrintCasePayload printCasePayload = mPrintCaseDirector.NewPayload();

            foreach (INode fact in caseModel.FactNodes)
                mPrintCaseDirector.Visit(fact, printCasePayload);

            return printCasePayload.GetString();
        }
    }
}