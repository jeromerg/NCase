using NDsl.Front.Ui;

namespace NCase.Front.Ui
{
    public class PrintCaseTable : IOp<ICaseEnumerable, string>
    {
        public static readonly PrintCaseTable Default = new PrintCaseTable();

        public bool RecurseIntoReferences { get; set; }
    }
}