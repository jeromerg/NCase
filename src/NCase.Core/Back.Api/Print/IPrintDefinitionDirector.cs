using JetBrains.Annotations;
using NDsl.Back.Api.Core;
using NVisitor.Api.Action;

namespace NCase.Back.Api.Print
{
    public interface IPrintDefinitionDirector : IActionDirector<INode, IPrintDefinitionDirector>
    {
        bool RecurseIntoReferences { get; set; }
        bool IncludeFileInfo { get; set; }
        string IndentationString { get; set; }

        void Indent();

        void Dedent();

        [StringFormatMethod("args")]
        void Print(CodeLocation codeLocation, string format, params object[] args);

        [StringFormatMethod("args")]
        void Print(string format, params object[] args);

        string GetString();
    }
}