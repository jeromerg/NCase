using NCase.Back.Api.Core;
using NCase.Back.Api.Parse;
using NCase.Back.Api.Tree;
using NDsl.Back.Api.Block;
using NDsl.Back.Api.Core;
using NDsl.Back.Api.Ref;

namespace NCase.Back.Imp.Tree
{
    public class ParseVisitors
        : IParseVisitor<BeginToken<TreeId>>,
          IParseVisitor<EndToken<TreeId>>,
          IParseVisitor<RefToken<TreeId>>
    {
        public void Visit(IParseDirector dir, BeginToken<TreeId> token)
        {
            var newCaseSetNode = new TreeNode(token.CodeLocation, token.Owner, null);

            dir.AddId(token.Owner, newCaseSetNode);
            dir.PushScope(newCaseSetNode);
        }

        public void Visit(IParseDirector dir, EndToken<TreeId> token)
        {
            dir.PopScope();
        }

        public void Visit(IParseDirector dir, RefToken<TreeId> token)
        {
            ICodeLocation codeLocation = token.CodeLocation;

            IDefNode referredSetNode = dir.GetReferencedNode<ITreeNode>(token.Owner, codeLocation);

            var newNode = new RefNode<ITreeNode>((ITreeNode) referredSetNode, codeLocation);

            dir.AddChildToScope(newNode);
        }
    }
}