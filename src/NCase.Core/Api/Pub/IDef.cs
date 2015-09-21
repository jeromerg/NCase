namespace NCase.Api.Pub
{
    /// <summary>Case Set Definition</summary>
    public interface IDef
    {
        ISet Cases { get; }
        TResult Get<TResult>(IDefTransform<TResult> transform);
    }
}