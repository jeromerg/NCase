using System.Text;

namespace NDsl.Back.Api.Util
{
    public interface ITableBuilder
    {
        void GenerateTable(StringBuilder sb);
        void NewRow();
        void Print(ITableColumn tableColumn, string format, params object[] args);
    }
}