using System.Text;
using JetBrains.Annotations;

namespace NDsl.Back.Api.Util.Table
{
    public interface ITableBuilder
    {
        void GenerateTable([NotNull] StringBuilder sb);
        void NewRow();
        void Print([NotNull] ITableColumn tableColumn, [NotNull] string format, [NotNull] params object[] args);
    }
}