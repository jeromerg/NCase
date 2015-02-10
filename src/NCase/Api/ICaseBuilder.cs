using System.Collections.Generic;
using NVisitor.Api.Lazy;
using NVisitor.Common.Quality;

namespace NCase.Api
{
    public interface ICaseBuilder
    {
        [NotNull] CaseSet CaseSet([NotNull] string name);
        [NotNull] T GetContributor<T>(string name);
        [NotNull] IEnumerable<Pause> PlayAllCases();
    }
}
