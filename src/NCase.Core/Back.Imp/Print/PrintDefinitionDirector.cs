using System;
using System.Text;
using NCase.Back.Api.Print;
using NDsl.Back.Api.Core;
using NVisitor.Api.Action;

namespace NCase.Back.Imp.Print
{
    public class PrintDefinitionDirector : ActionDirector<INode, IPrintDefinitionDirector>, IPrintDefinitionDirector
    {
        private readonly StringBuilder mStringBuilder = new StringBuilder();
        private int mIndentation;

        public PrintDefinitionDirector(IActionVisitMapper<INode, IPrintDefinitionDirector> visitMapper)
            : base(visitMapper)
        {
        }

        public string IndentationString { get; set; }
        public bool RecurseIntoReferences { get; set; }
        public bool IncludeFilePath { get; set; }
        public bool IncludeFileLineColumn { get; set; }

        public void Indent()
        {
            mIndentation++;
        }

        public void Dedent()
        {
            mIndentation--;
        }

        public void Print(CodeLocation codeLocation, string format, params object[] args)
        {
            Print("{0}{1}{2}",
                  IncludeFilePath ? codeLocation.GetFullInfo() + Environment.NewLine : "",
                  string.Format(format, args),
                  IncludeFileLineColumn ? " " + codeLocation.GetLineAndColumnInfo() : "");
        }

        public void Print(string format, params object[] args)
        {
            string s = string.Format(format, args);
            string[] lines = s.Split(new[] {Environment.NewLine}, StringSplitOptions.None);


            foreach (string line in lines)
            {
                for (int i = 0; i < mIndentation; i++)
                    mStringBuilder.Append(IndentationString);

                mStringBuilder.AppendLine(line);
            }
        }

        public string GetString()
        {
            return mStringBuilder.ToString();
        }
    }
}