using System.Collections.Generic;
using NCase.Front.Api;
using NDsl.Front.Ui;

namespace NCase.Front.Ui
{
    public interface ICaseEnumerable : IArtefact<ICaseEnumerableApi>, IEnumerable<ICase>
    {
    }
}