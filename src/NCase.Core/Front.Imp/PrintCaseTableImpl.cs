using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCase.Back.Api.Parse;
using NCase.Back.Api.Print;
using NCase.Front.Ui;
using NDsl.Back.Api.Core;
using NDsl.Front.Api;

namespace NCase.Front.Imp
{
    public class PrintCaseTableImpl : IOperationImp<ICase, PrintCaseTable, ICaseImp, string>
    {
        private readonly Func<IPrintCaseTableDirector> mPrintCaseTableDirectorFactory;

        public PrintCaseTableImpl([NotNull] Func<IPrintCaseTableDirector> printCaseTableDirectorFactory)
        {
            if (printCaseTableDirectorFactory == null) throw new ArgumentNullException("printCaseTableDirectorFactory");
            mPrintCaseTableDirectorFactory = printCaseTableDirectorFactory;
        }

        public string Perform(IOperationDirector director, PrintCaseTable printCaseTable, ICaseImp caseImp)
        {
            IPrintCaseTableDirector printDefinitionDirector = mPrintCaseTableDirectorFactory();

            CopyProperties(printCaseTable, printDefinitionDirector);

            foreach (IEnumerable<INode> @case in caseImp.Cases)
                foreach (INode fact in @case)
                    printDefinitionDirector.Visit(fact);

            return printDefinitionDirector.GetString();
        }

        private void CopyProperties(PrintCaseTable uiDef, IPrintCaseTableDirector dir)
        {
            dir.IncludeFileInfo = uiDef.IncludeFileInfo;
            dir.RecurseIntoReferences = uiDef.RecurseIntoReferences;
        }
    }
}