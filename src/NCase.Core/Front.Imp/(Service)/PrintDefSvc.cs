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
        [NotNull] private readonly IPrintDefinitionDirector mPrintDefinitionDirector;
        [NotNull] private readonly IPrintDefinitionPayloadFactory mPrintDefinitionPayloadFactory;

        public PrintDefSvc([NotNull] IParserGenerator parserGenerator,
                           [NotNull] IPrintDefinitionDirector printDefinitionDirector,
                           [NotNull] IPrintDefinitionPayloadFactory printDefinitionPayloadFactory)
        {
            mParserGenerator = parserGenerator;
            mPrintDefinitionDirector = printDefinitionDirector;
            mPrintDefinitionPayloadFactory = printDefinitionPayloadFactory;
        }

        [NotNull]
        public string PrintDef([NotNull] ISetDefModel<ISetDefId> setDefModel, bool isFileInfo, bool isRecursive)
        {
            using (setDefModel.TokenStream.SetReadMode())
            {
                INode caseSetNode = mParserGenerator.Parse(setDefModel.Id, setDefModel.TokenStream);

                IPrintDefinitionPayload payload = mPrintDefinitionPayloadFactory.Create(isFileInfo, isRecursive);

                mPrintDefinitionDirector.Visit(caseSetNode, payload);

                return payload.GetString();
            }
        }
    }
}