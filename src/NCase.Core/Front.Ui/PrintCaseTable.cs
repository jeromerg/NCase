using NDsl.Front.Ui;

namespace NCase.Front.Ui
{
    public class PrintCaseTable : IOp<ICase, string>
    {
        public static readonly PrintCaseTable Default = new PrintCaseTable();

        public bool RecurseIntoReferences { get; set; }
        public bool IncludeFileInfo { get; set; }
    }
}