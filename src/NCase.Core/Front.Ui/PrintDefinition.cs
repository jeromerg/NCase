using NDsl.Front.Ui;

namespace NCase.Front.Ui
{
    public class PrintDefinition : IOp<ISetDef, string>
    {
        public static readonly PrintDefinition Default = new PrintDefinition();

        public PrintDefinition()
        {
            Indentation = "    ";
            IsRecursive = false;
            IncludeFileInfo = false;
        }

        public string Indentation { get; set; }
        public bool IsRecursive { get; set; }
        public bool IncludeFileInfo { get; set; }
    }
}