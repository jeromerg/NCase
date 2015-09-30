using System.Collections.Generic;
using NCase.Front.Ui;
using NDsl.Back.Api.Core;

namespace NCase.Front.Imp
{
    public interface ICaseFactory
    {
        ICase Create(IEnumerable<INode> facts);
    }
}