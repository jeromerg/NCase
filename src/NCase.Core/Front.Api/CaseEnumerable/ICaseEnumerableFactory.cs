using System.Collections.Generic;
using JetBrains.Annotations;
using NCase.Front.Ui;
using NDsl.Back.Api.Common;

namespace NCase.Front.Api.CaseEnumerable
{
    public interface ICaseEnumerableFactory
    {
        ICaseEnumerable Create([NotNull] IEnumerable<List<INode>> cases);
    }
}