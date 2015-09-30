using System.Text;
using NDsl.Back.Api.Core;
using NVisitor.Api.ActionPayload;

namespace NCase.Back.Api.Print
{
    public interface IPrintDefinitionDirector : IActionPayloadDirector<INode, IPrintDefinitionDirector, StringBuilder>
    {
    }


    public class PrintDefinitionBuilder
    {
        #region inner types

        private class PrintDefinitionSettings
        {
            private readonly string mIndentationString;
            private readonly bool mRecurseIntoReferences;
            private readonly bool mIncludeFilePath;
            private readonly bool mIncludeFileRowColumn;


            public PrintDefinitionSettings(string indentationString,
                                           bool recurseIntoReferences,
                                           bool includeFilePath,
                                           bool includeFileRowColumn)
            {
                mIndentationString = indentationString;
                mRecurseIntoReferences = recurseIntoReferences;
                mIncludeFilePath = includeFilePath;
                mIncludeFileRowColumn = includeFileRowColumn;
            }
        }

        #endregion

        private readonly int mIndentation;
        private readonly PrintDefinitionSettings mSettings;

        public PrintDefinitionBuilder(string indentationString,
                                      bool recurseIntoReferences,
                                      bool includeFilePath,
                                      bool includeFileRowColumn,
                                      int indentation = 0)
        {
            mIndentation = indentation;
            mSettings = new PrintDefinitionSettings(indentationString,
                                                    recurseIntoReferences,
                                                    includeFilePath,
                                                    includeFileRowColumn);
        }

        private PrintDefinitionBuilder(PrintDefinitionSettings settings, int indentation = 0)
        {
            mSettings = settings;
            mIndentation = indentation;
        }

        public void Print(ICodeLocation codeLocation, string fact)
        {
            
        }
    }
}