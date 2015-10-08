using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCase.Back.Api.Parse;
using NCase.Back.Api.Print;
using NCase.Front.Ui;
using NDsl.Back.Api.Core;
using NDsl.Front.Api;

namespace NCase.Front.Imp.Op
{
    public class PrintTable : IOperationImp<ISetDef, Ui.PrintTable, ISetDefImp, string>
    {
        [NotNull] private readonly IParserGenerator mParserGenerator;
        [NotNull] private readonly Func<IPrintCaseTableDirector> mPrintCaseTableDirectorFactory;

        public PrintTable([NotNull] IParserGenerator parserGenerator,
                              [NotNull] Func<IPrintCaseTableDirector> printCaseTableDirectorFactory
            )
        {
            if (parserGenerator == null) throw new ArgumentNullException("parserGenerator");
            if (printCaseTableDirectorFactory == null) throw new ArgumentNullException("printCaseTableDirectorFactory");
            mParserGenerator = parserGenerator;
            mPrintCaseTableDirectorFactory = printCaseTableDirectorFactory;
        }

        public string Perform(IOperationDirector director, Ui.PrintTable printTable, ISetDefImp setDef)
        {
            INode setDefNode = mParserGenerator.Parse(setDef.DefId, setDef.Book);
            IEnumerable<List<INode>> cases = mParserGenerator.Generate(setDefNode, new GenerateOptions(printTable.IsRecursive));

            IPrintCaseTableDirector printCaseTableDirector = mPrintCaseTableDirectorFactory();

            foreach (List<INode> @case in cases)
            {
                printCaseTableDirector.NewRow();
                foreach (INode fact in @case)
                    printCaseTableDirector.Visit(fact);
            }
            return printCaseTableDirector.GetString();
        }
    }
}