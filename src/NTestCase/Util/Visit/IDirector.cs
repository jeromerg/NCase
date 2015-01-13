using NTestCase.Util.Quality;

namespace NTestCase.Util.Visit
{
    /// <summary>Used a marker, to lookup assemblies for Directors (i.e. by using DI-container) </summary>
    public interface IDirector
    {
    }

    public interface IDirector<in TNod> : IDirector
    {
        void Visit([NotNull] TNod node);
    }
}