using System;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.Parse;
using NCaseFramework.Back.Api.Print;
using NCaseFramework.Back.Api.SetDef;
using NCaseFramework.Front.Api.SetDef;
using NDsl.Back.Api.Common;

namespace NCaseFramework.Front.Imp
{
    public class PrintDefSvc : IPrintDefSvc
    {
        [NotNull] private readonly IParserGenerator mParserGenerator;
        [NotNull] private readonly Func<IPrintDefinitionDirector> mPrintDefinitionDirectorFactory;

        public PrintDefSvc([NotNull] IParserGenerator parserGenerator,
                           [NotNull] Func<IPrintDefinitionDirector> printDefinitionDirectorFactory)
        {
            mParserGenerator = parserGenerator;
            mPrintDefinitionDirectorFactory = printDefinitionDirectorFactory;
        }

        [NotNull]
        public string PrintDef([NotNull] ISetDefModel<ISetDefId> setDefModel, bool isFileInfo, bool isRecursive)
        {
            using (setDefModel.TokenStream.SetReadMode())
            {
                INode caseSetNode = mParserGenerator.Parse(setDefModel.Id, setDefModel.TokenStream);

                IPrintDefinitionDirector printDefinitionDirector = mPrintDefinitionDirectorFactory();

                if (printDefinitionDirector == null)
                    throw new ArgumentException("printDefinitionDirector == null");

                printDefinitionDirector.IsFileInfo = isFileInfo;
                printDefinitionDirector.IsRecursive = isRecursive;

                printDefinitionDirector.Visit(caseSetNode);

                return printDefinitionDirector.GetString();
            }
        }
    }
}