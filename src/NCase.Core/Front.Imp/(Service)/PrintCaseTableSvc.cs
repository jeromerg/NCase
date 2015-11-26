using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.Parse;
using NCaseFramework.Back.Api.Print;
using NCaseFramework.Back.Api.SetDef;
using NCaseFramework.Front.Api.SetDef;
using NDsl.Back.Api.Common;

namespace NCaseFramework.Front.Imp
{
    public class PrintCaseTableSvc : IPrintCaseTableSvc
    {
        [NotNull] private readonly IParserGenerator mParserGenerator;
        [NotNull] private readonly Func<IPrintCaseTableDirector> mPrintCaseTableDirectorFactory;

        public PrintCaseTableSvc([NotNull] IParserGenerator parserGenerator,
                                 [NotNull] Func<IPrintCaseTableDirector> printCaseTableDirectorFactory)
        {
            mParserGenerator = parserGenerator;
            mPrintCaseTableDirectorFactory = printCaseTableDirectorFactory;
        }

        [NotNull]
        public string Perform([NotNull] ISetDefModel<ISetDefId> setDefModel, bool isRecursive)
        {
            if (setDefModel == null) throw new ArgumentNullException("setDefModel");

            INode setDefNode = mParserGenerator.Parse(setDefModel.Id, setDefModel.TokenStream);
            IEnumerable<List<INode>> cases = mParserGenerator.Generate(setDefNode, new GenerateOptions(isRecursive));

            IPrintCaseTableDirector printCaseTableDirector = mPrintCaseTableDirectorFactory();

            if (printCaseTableDirector == null)
                throw new ArgumentException("printCaseTableDirector == null");

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