using NCase.Api.Dev.Core;
using NVisitor.Common.Quality;

namespace NCase.Api.Pub
{
    public interface IBuilder
    {
        [NotNull] T CreateSet<T>([NotNull] string name) where T : IDef;
        [NotNull] T CreateContributor<T>([NotNull] string name);
    }
}
