using JetBrains.Annotations;
using NDsl.All.Common;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Util;
using NVisitor.Api.Action;

namespace NCaseFramework.Back.Api.Parse
{
    public interface IParseDirector : IActionDirector<IToken, IParseDirector>
    {
        void AddId([NotNull] IId id, [NotNull] INode referencedNode);
        [NotNull] TNod GetNodeForId<TNod>([NotNull] IId id, [NotNull] CodeLocation location) where TNod : INode;

        void PushScope([NotNull] INode rootNode);
        void PopScope();
        void AddChildToScope([NotNull] INode childNode);
    }
}