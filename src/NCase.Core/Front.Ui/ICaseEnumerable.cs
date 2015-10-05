using System.Collections.Generic;
using NDsl.Front.Ui;

namespace NCase.Front.Ui
{
    public interface ICaseEnumerable : IArtefact<ICaseEnumerable>, IEnumerable<ICase>
    {
    }
}