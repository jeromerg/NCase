using System.Text;

namespace NCase.Back.Imp.Print
{
    public interface ITableBuilder
    {
        void GenerateTable(StringBuilder sb);
        void NewRow();
        void Print(ITableColumn tableColumn, string format, params object[] args);
    }
}