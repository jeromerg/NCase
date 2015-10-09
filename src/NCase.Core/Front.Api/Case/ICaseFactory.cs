using System.Collections.Generic;
using JetBrains.Annotations;
using NCase.Front.Ui;
using NDsl.Back.Api.Common;

namespace NCase.Front.Api.Case
{
    public interface ICaseFactory
    {
        ICase Create([NotNull] IEnumerable<INode> factNodes);
    }
}