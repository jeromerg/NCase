using JetBrains.Annotations;
using NCaseFramework.Back.Api.Parse;
using NCaseFramework.Back.Api.Tree;
using NDsl.Back.Api.Def;
using NDsl.Back.Api.Ref;
using NDsl.Back.Api.Util;

namespace NCaseFramework.Back.Imp.Tree
{
    public class ParseVisitors
        : IParseVisitor<BeginToken<TreeId>>,
          IParseVisitor<EndToken<TreeId>>,
          IParseVisitor<RefToken<TreeId>>
    {
        public void Visit([NotNull] IParseDirector dir, [NotNull] BeginToken<TreeId> token)
        {
            var newCaseSetNode = new TreeNode(token.CodeLocation, token.Owner, null);

            dir.AddId(token.Owner, newCaseSetNode);
            dir.PushScope(newCaseSetNode);
        }

        public void Visit([NotNull] IParseDirector dir, [NotNull] EndToken<TreeId> token)
        {
            dir.PopScope();
        }

        public void Visit([NotNull] IParseDirector dir, [NotNull] RefToken<TreeId> token)
        {
            CodeLocation codeLocation = token.CodeLocation;

            IDefNode referredSetNode = dir.GetNodeForId<ITreeNode>(token.Owner, codeLocation);

            var newNode = new RefNode<ITreeNode>((ITreeNode) referredSetNode, codeLocation);

            dir.AddChildToScope(newNode);
        }
    }
}