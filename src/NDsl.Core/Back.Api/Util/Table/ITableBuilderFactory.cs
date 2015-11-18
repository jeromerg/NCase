using JetBrains.Annotations;

namespace NDsl.Back.Api.Util.Table
{
    public interface ITableBuilderFactory
    {
        [NotNull]
        ITableBuilder Create();
    }
}