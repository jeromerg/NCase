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
    public class PrintTableImp : IPrintTable
    {
        [NotNull] private readonly IParserGenerator mParserGenerator;
        [NotNull] private readonly Func<IPrintCaseTableDirector> mPrintCaseTableDirectorFactory;

        public PrintTableImp([NotNull] IParserGenerator parserGenerator,
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
            INode setDefNode = mParserGenerator.Parse(setDefModel.Id, setDefModel.TokenStream);
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