using JetBrains.Annotations;
using NDsl.Back.Api.Util.Table;

namespace NDsl.Back.Imp.Util.Table
{
    public class TableBuilderFactory : ITableBuilderFactory
    {
        [NotNull] 
        public ITableBuilder Create()
        {
            return new TableBuilder();
        }
    }
}