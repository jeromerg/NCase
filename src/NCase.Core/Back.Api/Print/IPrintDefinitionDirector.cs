﻿using JetBrains.Annotations;
using NDsl.Back.Api.Core;
using NVisitor.Api.Action;

namespace NCase.Back.Api.Print
{
    public interface IPrintDefinitionDirector : IActionDirector<INode, IPrintDefinitionDirector>
    {
        bool IsRecursive { get; set; }
        bool IsFileInfo { set; }
        
        void Indent();

        void Dedent();

        [StringFormatMethod("args")]
        void PrintLine(CodeLocation codeLocation, string format, params object[] args);

        string GetString();
    }
}