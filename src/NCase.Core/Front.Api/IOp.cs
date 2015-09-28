namespace NCase.Front.Api
{
    public interface IOp { }

    /// <summary>Operation base interface</summary>
    /// <typeparam name="TArtefact">The artefact the operation applies to</typeparam>
    /// <typeparam name="TResult">The result of the operation</typeparam>
    public interface IOp<TArtefact, TResult> : IOp
    {
    }
}