namespace NDsl.Front.Api
{
    public interface IArtefact
    {
    }

    /// <summary>
    ///     Describes the artefact produced during the build and processing, which can be manipulated by the user
    /// </summary>
    /// <typeparam name="TArtefact"></typeparam>
    public interface IArtefact<out TArtefact> : IArtefact
        where TArtefact : IArtefact<TArtefact>
    {
        TResult Perform<TOp, TResult>(TOp operation) where TOp : IOp<TArtefact, TResult>;
    }
}