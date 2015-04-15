using System.Collections.Generic;
using NVisitor.Api.Lazy;
using NVisitor.Common.Quality;

namespace NCase.Api
{
    public interface ICaseBuilder
    {
        [NotNull] CaseSet CreateSet([NotNull] string name);
        [NotNull] T GetContributor<T>([NotNull] string name);
        [NotNull] IEnumerable<Pause> GetAllCases();
    }
}
