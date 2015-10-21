﻿using System.Collections.Generic;
using JetBrains.Annotations;
using NCaseFramework.Front.Ui;
using NDsl.Back.Api.Common;

namespace NCaseFramework.Front.Api.Case
{
    public interface ICaseFactory
    {
        Ui.Case Create([NotNull] IEnumerable<INode> factNodes);
    }
}