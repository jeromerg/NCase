using System.Collections.Generic;
using NVisitor.Api.Lazy;
using NVisitor.Common.Quality;

namespace NCase.Api
{
    public interface ICaseBuilder
    {
        [NotNull] T CreateSet<T>([NotNull] string name) where T : ICaseSet;
        [NotNull] T GetContributor<T>([NotNull] string name);
        [NotNull] IEnumerable<Pause> GetAllCases(ICaseSet caseSet);
    }
}
