namespace NDsl.Front.Ui
{
    /// <summary>Any Type that belongs to an NDsl workspace and have access to the workspace tools</summary>
    public interface IArtefact<out TApi>
        where TApi : IArtefactApi
    {
        TApi Api { get; }
    }
}