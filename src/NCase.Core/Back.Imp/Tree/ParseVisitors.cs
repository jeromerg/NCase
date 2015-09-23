using NCase.Back.Api.Core;
using NCase.Back.Api.Core.Parse;
using NCase.Back.Api.Tree;
using NCase.Front.Api;
using NDsl.Api.Dev.Core.Nod;
using NDsl.Api.Dev.Core.Tok;
using NDsl.Api.Dev.Core.Util;

namespace NCase.Back.Imp.Tree
{
    public class ParseVisitors
        : IParseVisitor<BeginToken<TreeId>>,
          IParseVisitor<EndToken<TreeId>>,
          IParseVisitor<RefToken<TreeId>>
    {
        public void Visit(IParseDirector dir, BeginToken<TreeId> token)
        {
            var newCaseSetNode = new TreeNode(token.CodeLocation, token.OwnerId, null);

            dir.AddId(token.OwnerId, newCaseSetNode);
            dir.PushScope(newCaseSetNode);
        }

        public void Visit(IParseDirector dir, EndToken<TreeId> token)
        {
            dir.PopScope();
        }

        public void Visit(IParseDirector dir, RefToken<TreeId> token)
        {
            ICodeLocation codeLocation = token.CodeLocation;

            IDefNode referredSetNode = dir.GetReferencedNode<ITreeNode>(token.OwnerId, codeLocation);

            var newNode = new RefNode<ITreeNode>((ITreeNode) referredSetNode, codeLocation);

            dir.AddChildToScope(newNode);
        }
    }
}