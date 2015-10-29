using System.Collections.Generic;
using JetBrains.Annotations;
using NDsl.Back.Api.Common;

namespace NCaseFramework.Front.Api.CaseEnumerable
{
    public interface ICaseEnumerableFactory
    {
        Ui.CaseEnumerable Create([NotNull] IEnumerable<List<INode>> cases);
    }
}