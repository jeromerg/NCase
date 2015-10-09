using System;
using JetBrains.Annotations;
using NCase.Back.Api.Parse;
using NCase.Back.Api.Print;
using NCase.Back.Api.SetDef;
using NCase.Front.Api.SetDef;
using NDsl.Back.Api.Common;

namespace NCase.Front.Imp.Tool
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

        public string Perform(ISetDefModel<ISetDefId> setDefModel, bool isFileInfo, bool isRecursive)
        {
            INode caseSetNode = mParserGenerator.Parse(setDefModel.Id, setDefModel.TokenStream);

            IPrintDefinitionDirector dir = mPrintDefinitionDirectorFactory();

            dir.IsFileInfo = isFileInfo;
            dir.IsRecursive = isRecursive;

            dir.Visit(caseSetNode);

            return dir.GetString();
        }
    }
}