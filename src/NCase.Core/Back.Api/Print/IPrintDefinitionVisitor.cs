﻿using NDsl.Back.Api.Common;
using NVisitor.Api.Action;

namespace NCaseFramework.Back.Api.Print
{
    public interface IPrintDefinitionVisitor<TNod> : IActionVisitor<INode, IPrintDefinitionDirector, TNod>
        where TNod : INode
    {
    }
}