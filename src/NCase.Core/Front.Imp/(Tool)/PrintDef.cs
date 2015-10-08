using System;
using JetBrains.Annotations;
using NCase.Back.Api.Parse;
using NCase.Back.Api.Print;
using NCase.Front.Api;
using NCase.Front.Ui;
using NDsl.All;
using NDsl.Back.Api.Core;

namespace NCase.Front.Imp.Op
{

    public class PrintDef : ITool<ISetDefApi>, IPrintDef
    {
        [NotNull] private readonly IParserGenerator mParserGenerator;
        private readonly Func<IPrintDefinitionDirector> mPrintDefinitionDirectorFactory;

        public PrintDef([NotNull] IParserGenerator parserGenerator,
                                   [NotNull] Func<IPrintDefinitionDirector> printDefinitionDirectorFactory)
        {
            if (parserGenerator == null) throw new ArgumentNullException("parserGenerator");
            if (printDefinitionDirectorFactory == null) throw new ArgumentNullException("printDefinitionDirectorFactory");
            mParserGenerator = parserGenerator;
            mPrintDefinitionDirectorFactory = printDefinitionDirectorFactory;
        }

        public ICaseEnumerable Perform(ISetDefApi setDefApi)
        {
            throw new NotImplementedException();
        }

        public string Perform(IOperationDirector director, PrintDefinition printDefinition, ISetDefImp setDefImp)
        {
            INode caseSetNode = mParserGenerator.Parse(setDefImp.DefId, setDefImp.Book);

            IPrintDefinitionDirector printDefinitionDirector = mPrintDefinitionDirectorFactory();

            CopyProperties(printDefinition, printDefinitionDirector);
            printDefinitionDirector.Visit(caseSetNode);

            return printDefinitionDirector.GetString();
        }

        private void CopyProperties(PrintDefinition uiDef, IPrintDefinitionDirector dir)
        {
            dir.IncludeFileInfo = uiDef.IncludeFileInfo;
            dir.IndentationString = uiDef.Indentation;
            dir.IsRecursive = uiDef.IsRecursive;
        }
    }
}