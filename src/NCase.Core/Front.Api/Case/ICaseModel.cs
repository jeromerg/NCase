using System.Collections.Generic;
using NDsl.Back.Api.Common;

namespace NCaseFramework.Front.Api.Case
{
    public interface ICaseModel
    {
        List<INode> FactNodes { get; }
    }
}