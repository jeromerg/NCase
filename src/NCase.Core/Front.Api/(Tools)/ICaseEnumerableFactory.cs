using System.Collections.Generic;
using JetBrains.Annotations;
using NCase.Front.Ui;
using NDsl.Back.Api.Core;

namespace NCase.Front.Imp
{
    public interface ICaseEnumerableFactory
    {
        ICaseEnumerable Create([NotNull] IEnumerable<List<INode>> cases);
    }
}