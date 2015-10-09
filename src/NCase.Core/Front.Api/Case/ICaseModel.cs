using System.Collections.Generic;
using NDsl.Back.Api.Common;
using NDsl.Front.Api;

namespace NCase.Front.Api.Case
{
    public interface ICaseModel : IArtefactModel
    {
        IEnumerable<INode> FactNodes { get; }
    }
}