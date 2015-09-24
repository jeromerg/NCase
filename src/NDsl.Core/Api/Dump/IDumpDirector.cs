using JetBrains.Annotations;
using NDsl.Api.Core;
using NVisitor.Api.Action;

namespace NDsl.Api.Dump
{
    public interface IDumpDirector : IActionDirector<INode, IDumpDirector>
    {
        void Indent();
        void Dedent();

        [StringFormatMethod("format")]
        void AddText(string format, params object[] formatArgs);
    }
}