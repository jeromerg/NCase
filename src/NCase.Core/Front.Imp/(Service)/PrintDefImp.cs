using System;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.Parse;
using NCaseFramework.Back.Api.Print;
using NCaseFramework.Back.Api.SetDef;
using NCaseFramework.Front.Api.SetDef;
using NDsl.Back.Api.Common;

namespace NCaseFramework.Front.Imp
{
    public class PrintDefImp : IPrintDef
    {
        [NotNull] private readonly IParserGenerator mParserGenerator;
        private readonly Func<IPrintDefinitionDirector> mPrintDefinitionDirectorFactory;

        public PrintDefImp([NotNull] IParserGenerator parserGenerator,
                           [NotNull] Func<IPrintDefinitionDirector> printDefinitionDirectorFactory)
        {
            if (parserGenerator == null) throw new ArgumentNullException("parserGenerator");
            if (printDefinitionDirectorFactory == null) throw new ArgumentNullException("printDefinitionDirectorFactory");
            mParserGenerator = parserGenerator;
            mPrintDefinitionDirectorFactory = printDefinitionDirectorFactory;
        }

        public string Perform(ISetDefModel<ISetDefId> setDefModel, bool isFileInfo, bool isRecursive)
        {
            using (setDefModel.TokenStream.SetReadMode())
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
}