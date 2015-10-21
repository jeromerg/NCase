using System.Collections.Generic;
using NDsl.Back.Api.Common;
using NDsl.Front.Api;

namespace NCaseFramework.Front.Api.CaseEnumerable
{
    public interface ICaseEnumerableModel : IArtefactModel
    {
        IEnumerable<List<INode>> Cases { get; }
    }
}