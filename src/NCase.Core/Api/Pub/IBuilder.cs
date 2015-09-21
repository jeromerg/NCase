using NVisitor.Common.Quality;

namespace NCase.Api.Pub
{
    public interface IBuilder
    {
        [NotNull]
        T CreateDef<T>([NotNull] string name) where T : IDef;

        [NotNull]
        T CreateContributor<T>([NotNull] string name);
    }
}