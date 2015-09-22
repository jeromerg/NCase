namespace NCase.Api.Pub
{
    /// <summary>Case Set Definition</summary>
    public interface IDef : IArtefact
    {
        ISet Cases { get; }
    }
}