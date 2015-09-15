using NDsl.Api.Dev.Core.Nod;
using NDsl.Api.Dev.Core.Tok;
using NDsl.Api.Dev.Core.Util;
using NVisitor.Api.Batch;

namespace NCase.Api.Dev.Core.Parse
{
    public interface IParseDirector : IDirector<IToken, IParseDirector>
    {
        void AddReference(object reference, INode referencedNode);
        TNod GetReference<TNod>(object reference, ICodeLocation location) where TNod : INode;

        void PushScope(INode rootNode);
        void PopScope();
        void AddChildToScope(INode childNode);
    }
}