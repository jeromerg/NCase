using NDsl.Back.Api.Common;
using NDsl.Back.Api.Util;
using NVisitor.Api.Action;

namespace NCase.Back.Api.Parse
{
    public interface IParseDirector : IActionDirector<IToken, IParseDirector>
    {
        void AddId(object reference, INode referencedNode);
        TNod GetReferencedNode<TNod>(object reference, CodeLocation location) where TNod : INode;

        void PushScope(INode rootNode);
        void PopScope();
        void AddChildToScope(INode childNode);
    }
}