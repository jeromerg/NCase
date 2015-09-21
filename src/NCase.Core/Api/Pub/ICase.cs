namespace NCase.Api.Pub
{
    public interface ICase : IArtefact
    {
        void Replay(bool isReplay);
    }
}