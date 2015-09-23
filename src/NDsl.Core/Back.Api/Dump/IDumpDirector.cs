using NDsl.Back.Api.Core;
using NVisitor.Api.Action;
using NVisitor.Common.Quality;

namespace NDsl.Back.Api.Dump
{
    public interface IDumpDirector : IActionDirector<INode, IDumpDirector>
    {
        void Indent();
        void Dedent();

        [StringFormatMethod("format")]
        void AddText(string format, params object[] formatArgs);
    }
}