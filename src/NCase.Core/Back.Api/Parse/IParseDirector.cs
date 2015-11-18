using NDsl.All.Common;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Util;
using NVisitor.Api.Action;

namespace NCaseFramework.Back.Api.Parse
{
    public interface IParseDirector : IActionDirector<IToken, IParseDirector>
    {
        void AddId(IId id, INode referencedNode);
        TNod GetNodeForId<TNod>(IId id, CodeLocation location) where TNod : INode;

        void PushScope(INode rootNode);
        void PopScope();
        void AddChildToScope(INode childNode);
    }
}