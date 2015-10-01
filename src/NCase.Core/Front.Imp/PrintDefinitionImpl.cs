using System;
using JetBrains.Annotations;
using NCase.Back.Api.Parse;
using NCase.Back.Api.Print;
using NCase.Front.Ui;
using NDsl.Back.Api.Core;
using NDsl.Front.Api;

namespace NCase.Front.Imp
{
    public class PrintDefinitionImpl : IOperationImp<ISetDef, PrintDefinition, ISetDefImp, string>
    {
        [NotNull] private readonly IParserGenerator mParserGenerator;
        private readonly Func<IPrintDefinitionDirector> mPrintDefinitionDirectorFactory;

        public PrintDefinitionImpl([NotNull] IParserGenerator parserGenerator,
                                   [NotNull] Func<IPrintDefinitionDirector> printDefinitionDirectorFactory)
        {
            if (parserGenerator == null) throw new ArgumentNullException("parserGenerator");
            if (printDefinitionDirectorFactory == null) throw new ArgumentNullException("printDefinitionDirectorFactory");
            mParserGenerator = parserGenerator;
            mPrintDefinitionDirectorFactory = printDefinitionDirectorFactory;
        }

        public string Perform(IOperationDirector director, PrintDefinition printDefinition, ISetDefImp setDefImp)
        {
            INode caseSetNode = mParserGenerator.Parse(setDefImp.DefId, setDefImp.TokenReaderWriter);

            IPrintDefinitionDirector printDefinitionDirector = mPrintDefinitionDirectorFactory();

            printDefinitionDirector.Visit(caseSetNode);

            return printDefinitionDirector.GetString();
        }
    }
}