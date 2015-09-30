using System;
using System.Collections.Generic;
using System.Text;
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
        private readonly IPrintDefinitionDirector mPrintDefinitionDirector;

        public PrintDefinitionImpl([NotNull] IParserGenerator parserGenerator,
                                   [NotNull] IPrintDefinitionDirector printDefinitionDirector)
        {
            if (parserGenerator == null) throw new ArgumentNullException("parserGenerator");
            if (printDefinitionDirector == null) throw new ArgumentNullException("printDefinitionDirector");
            mParserGenerator = parserGenerator;
            mPrintDefinitionDirector = printDefinitionDirector;
        }

        public string Perform(IOperationDirector director, PrintDefinition printDefinition, ISetDefImp setDefImp)
        {
            INode caseSetNode = mParserGenerator.Parse(setDefImp.DefId, setDefImp.TokenReaderWriter);

            var stringBuilder = new StringBuilder();
            mPrintDefinitionDirector.Visit(caseSetNode, stringBuilder);

            return stringBuilder.ToString();
        }
    }

}