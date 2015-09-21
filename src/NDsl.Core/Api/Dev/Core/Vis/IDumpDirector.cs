using NDsl.Api.Dev.Core.Nod;
using NVisitor.Api.Action;
using NVisitor.Common.Quality;

namespace NDsl.Api.Dev.Core.Vis
{
    public interface IDumpDirector : IActionDirector<INode, IDumpDirector>
    {
        void Indent();
        void Dedent();

        [StringFormatMethod("format")] void AddText(string format, params object[] formatArgs);
    }
}