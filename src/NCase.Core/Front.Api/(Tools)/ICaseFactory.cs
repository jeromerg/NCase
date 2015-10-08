using System.Collections.Generic;
using JetBrains.Annotations;
using NCase.Front.Ui;
using NDsl.Back.Api.Core;

namespace NCase.Front.Imp
{
    public interface ICaseFactory
    {
        ICase Create([NotNull] IEnumerable<INode> factNodes);
    }
}