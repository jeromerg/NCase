using System.Collections.Generic;
using NCase.Front.Api;
using NCase.Front.Api.CaseEnumerable;
using NDsl.Front.Api;

namespace NCase.Front.Ui
{
    public interface ICaseEnumerable : IArtefact<ICaseEnumerableModel>, IEnumerable<ICase>
    {
    }
}