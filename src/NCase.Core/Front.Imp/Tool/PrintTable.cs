using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCase.Back.Api.Parse;
using NCase.Back.Api.Print;
using NCase.Back.Api.SetDef;
using NCase.Front.Api.SetDef;
using NDsl.Back.Api.Common;

namespace NCase.Front.Imp.Tool
{
    public class PrintTable : IPrintTable
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

        public string Perform(ISetDefModel<ISetDefId> setDefModel, bool isRecursive)
        {
            INode setDefNode = mParserGenerator.Parse(setDefModel.Id, setDefModel.Book);
            IEnumerable<List<INode>> cases = mParserGenerator.Generate(setDefNode, new GenerateOptions(isRecursive));

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