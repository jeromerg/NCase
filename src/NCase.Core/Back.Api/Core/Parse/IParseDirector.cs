using NDsl.Api.Dev.Core.Nod;
using NDsl.Api.Dev.Core.Tok;
using NDsl.Api.Dev.Core.Util;
using NVisitor.Api.Action;

namespace NCase.Back.Api.Core.Parse
{
    public interface IParseDirector : IActionDirector<IToken, IParseDirector>
    {
        void AddId(object reference, INode referencedNode);
        TNod GetReferencedNode<TNod>(object reference, ICodeLocation location) where TNod : INode;

        void PushScope(INode rootNode);
        void PopScope();
        void AddChildToScope(INode childNode);
    }
}