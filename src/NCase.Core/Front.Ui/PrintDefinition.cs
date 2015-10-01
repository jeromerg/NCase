using NDsl.Front.Ui;

namespace NCase.Front.Ui
{
    public class PrintDefinition : IOp<ISetDef, string>
    {
        public static readonly PrintDefinition Default = new PrintDefinition();

        public PrintDefinition()
        {
            Indentation = "    ";
            RecurseIntoReferences = false;
            IncludeFilePath = false;
            IncludeFileRowColumn = true;
        }

        public string Indentation { get; set; }
        public bool RecurseIntoReferences { get; set; }
        public bool IncludeFilePath { get; set; }
        public bool IncludeFileRowColumn { get; set; }
    }
}