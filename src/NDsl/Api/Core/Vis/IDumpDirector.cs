using NDsl.Api.Core.Nod;
using NVisitor.Api.Batch;
using NVisitor.Common.Quality;

namespace NDsl.Api.Core.Vis
{
    public interface IDumpDirector : IDirector<INode, IDumpDirector>
    {
        void Indent();
        void Dedent();
        [StringFormatMethod("format")]
        void AddText(string format, params object[] formatArgs);
    }
}
