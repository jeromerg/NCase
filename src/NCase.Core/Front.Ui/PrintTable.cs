using NDsl.Front.Ui;

namespace NCase.Front.Ui
{
    public class PrintTable : IOp<ISetDef, string>
    {
        public static readonly PrintTable Default = new PrintTable();

        public PrintTable()
        {
            IsRecursive = true;
        }

        public bool IsRecursive { get; set; }
    }
}