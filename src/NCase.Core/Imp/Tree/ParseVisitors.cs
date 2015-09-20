using NCase.Api;
using NCase.Api.Dev.Core;
using NCase.Api.Dev.Core.Parse;
using NCase.Api.Dev.Tree;
using NDsl.Api.Dev.Core.Nod;
using NDsl.Api.Dev.Core.Tok;
using NDsl.Api.Dev.Core.Util;

namespace NCase.Imp.Tree
{
    public class ParseVisitors
        : IParseVisitor<BeginToken<ITree>>
        , IParseVisitor<EndToken<ITree>>
        , IParseVisitor<RefToken<ITree>>
    {
        public void Visit(IParseDirector dir, BeginToken<ITree> token)
        {
            var newCaseSetNode = new TreeNode(token.CodeLocation, token.Owner, null);
            
            dir.AddReference(token.Owner, newCaseSetNode);
            dir.PushScope(newCaseSetNode);
        }

        public void Visit(IParseDirector dir, EndToken<ITree> token)
        {
            dir.PopScope();
        }

        public void Visit(IParseDirector dir, RefToken<ITree> token)
        {
            ICodeLocation codeLocation = token.CodeLocation;

            IDefNode referredSetNode = dir.GetReference<ITreeNode>(token.Owner, codeLocation);

            var newNode = new RefNode<ITreeNode>((ITreeNode) referredSetNode, codeLocation);

            dir.AddChildToScope(newNode);
        }
    }
}