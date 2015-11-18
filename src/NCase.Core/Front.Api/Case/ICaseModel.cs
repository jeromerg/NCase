using System.Collections.Generic;
using JetBrains.Annotations;
using NDsl.Back.Api.Common;

namespace NCaseFramework.Front.Api.Case
{
    public interface ICaseModel
    {
        [NotNull, ItemNotNull] 
        List<INode> FactNodes { get; }
    }
}