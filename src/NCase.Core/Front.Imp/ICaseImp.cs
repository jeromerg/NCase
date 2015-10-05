using System.Collections.Generic;
using NDsl.Back.Api.Core;
using NDsl.Front.Api;

namespace NCase.Front.Imp
{
    public interface ICaseImp : IArtefactImp
    {
        IEnumerable<IEnumerable<INode>> Cases { get; }
    }
}