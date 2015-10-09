namespace NDsl.Back.Api.Util.Table
{
    /// <summary>
    ///     Remark: Equals and GetHashCode must allow identifying the column. ToString provides the Header title
    /// </summary>
    public interface ITableColumn
    {
        HorizontalAlignment HorizontalAlignment { get; }
        string Title { get; }
    }
}