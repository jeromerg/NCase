using System.Collections.Generic;
using NDsl.Back.Api.Core;
using NDsl.Front.Ui;

namespace NCase.Front.Api
{
    public interface ICaseApi : IArtefactApi
    {
        IEnumerable<INode> FactNodes { get; }
    }
}