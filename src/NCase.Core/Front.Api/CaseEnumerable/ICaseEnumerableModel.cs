using System.Collections.Generic;
using NDsl.Back.Api.Common;
using NDsl.Front.Api;

namespace NCase.Front.Api.CaseEnumerable
{
    public interface ICaseEnumerableModel : IArtefactModel
    {
        IEnumerable<List<INode>> Cases { get; }
    }
}