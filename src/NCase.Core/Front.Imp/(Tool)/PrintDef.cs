using System;
using JetBrains.Annotations;
using NCase.Back.Api.Parse;
using NCase.Back.Api.Print;
using NCase.Back.Api.Tree;
using NCase.Front.Api;
using NCase.Front.Ui;
using NDsl.All;
using NDsl.Back.Api.Core;

namespace NCase.Front.Imp.Op
{

    public class PrintDef : IPrintDef
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

        public string Perform(ISetDefApi<ISetDefApi, ISetDefId> setDefApi, bool isFileInfo, bool isRecursive)
        {
            INode caseSetNode = mParserGenerator.Parse(setDefApi.Id, setDefApi.Book);

            IPrintDefinitionDirector dir = mPrintDefinitionDirectorFactory();

            dir.IsFileInfo = isFileInfo;
            dir.IsRecursive = isRecursive;

            dir.Visit(caseSetNode);

            return dir.GetString();
        }
    }
}