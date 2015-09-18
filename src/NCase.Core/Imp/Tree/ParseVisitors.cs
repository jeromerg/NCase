using NCase.Api.Dev.Core;
using NCase.Api.Dev.Core.Parse;
using NCase.Api.Dev.Tree;
using NDsl.Api.Dev.Core.Nod;
using NDsl.Api.Dev.Core.Tok;
using NDsl.Api.Dev.Core.Util;

namespace NCase.Imp.Tree
{
    public class ParseVisitors
        : IParseVisitor<BeginToken<TreeCaseSet>>
        , IParseVisitor<EndToken<TreeCaseSet>>
        , IParseVisitor<RefToken<TreeCaseSet>>
    {
        public void Visit(IParseDirector dir, BeginToken<TreeCaseSet> token)
        {
            var newCaseSetNode = new TreeNode(
                token.CodeLocation, 
                token.Owner, 
                null /*root without associated fact*/);
            
            dir.AddReference(token.Owner, newCaseSetNode);
            dir.PushScope(newCaseSetNode);
        }

        public void Visit(IParseDirector dir, EndToken<TreeCaseSet> token)
        {
            dir.PopScope();
        }

        public void Visit(IParseDirector dir, RefToken<TreeCaseSet> token)
        {
            ICodeLocation codeLocation = token.CodeLocation;

            ICaseSetNode referredSetNode = dir.GetReference<ITreeNode>(token.Owner, codeLocation);

            var newNode = new RefNode<ITreeNode>((ITreeNode) referredSetNode, codeLocation);

            dir.AddChildToScope(newNode);
        }
    }
}