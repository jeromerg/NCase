namespace NCase.Front.Api
{
    public interface IDef : IArtefact
    {
    }

    /// <summary>Case Set Definition</summary>
    public interface IDef<out TDef> : IArtefact<TDef>, IDef
        where TDef : IDef<TDef>
    {
    }
}