using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCase.Back.Api.Print;
using NCase.Front.Imp.Artefact;
using NCase.Front.Ui;
using NDsl.Back.Api.Core;
using NDsl.Front.Api;

namespace NCase.Front.Imp.Op
{
    public class PrintCaseTableImpl : IOperationImp<ICaseEnumerable, PrintCaseTable, CaseEnumerableImp, string>
    {
        [NotNull] private readonly Func<IPrintCaseTableDirector> mPrintCaseTableDirectorFactory;

        public PrintCaseTableImpl([NotNull] Func<IPrintCaseTableDirector> printCaseTableDirectorFactory)
        {
            if (printCaseTableDirectorFactory == null) throw new ArgumentNullException("printCaseTableDirectorFactory");
            mPrintCaseTableDirectorFactory = printCaseTableDirectorFactory;
        }

        public string Perform(IOperationDirector director, PrintCaseTable printCaseTable, CaseEnumerableImp caseEnumerable)
        {
            IPrintCaseTableDirector printCaseTableDirector = mPrintCaseTableDirectorFactory();

            CopyProperties(printCaseTable, printCaseTableDirector);

            foreach (List<INode> @case in caseEnumerable.Cases)
            {
                printCaseTableDirector.NewRow();
                foreach (INode fact in @case)
                    printCaseTableDirector.Visit(fact);
            }
            return printCaseTableDirector.GetString();
        }

        private void CopyProperties(PrintCaseTable uiDef, IPrintCaseTableDirector dir)
        {
            dir.RecurseIntoReferences = uiDef.RecurseIntoReferences;
        }
    }
}