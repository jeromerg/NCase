using JetBrains.Annotations;
using NCaseFramework.Back.Api.Combinations;
using NCaseFramework.Back.Api.Parse;
using NDsl.Back.Api.Def;
using NDsl.Back.Api.Ref;
using NDsl.Back.Api.Util;

namespace NCaseFramework.Back.Imp.Combinations
{
    public class ParseVisitors
        : IParseVisitor<BeginToken<ProdId>>,
          IParseVisitor<EndToken<ProdId>>,
          IParseVisitor<RefToken<ProdId>>
    {
        public void Visit([NotNull] IParseDirector dir, [NotNull] BeginToken<ProdId> token)
        {
            var newCaseSetNode = new ProdNode(token.CodeLocation, token.Owner, null);

            dir.AddId(token.Owner, newCaseSetNode);
            dir.PushScope(newCaseSetNode);
        }

        public void Visit([NotNull] IParseDirector dir, [NotNull] EndToken<ProdId> token)
        {
            dir.PopScope();
        }

        public void Visit([NotNull] IParseDirector dir, [NotNull] RefToken<ProdId> token)
        {
            CodeLocation codeLocation = token.CodeLocation;

            IDefNode referredSetNode = dir.GetNodeForId<IProdNode>(token.Owner, codeLocation);

            var newNode = new RefNode<IProdNode>((IProdNode)referredSetNode, codeLocation);

            dir.AddChildToScope(newNode);
        }
    }
}