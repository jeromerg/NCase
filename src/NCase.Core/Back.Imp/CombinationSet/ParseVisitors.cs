using JetBrains.Annotations;
using NCaseFramework.Back.Api.Combinations;
using NCaseFramework.Back.Api.Parse;
using NDsl.Back.Api.Def;
using NDsl.Back.Api.Ref;
using NDsl.Back.Api.Util;

namespace NCaseFramework.Back.Imp.Combinations
{
    public class ParseVisitors
        : IParseVisitor<CombinationSetBeginToken>,
          IParseVisitor<EndToken<CombinationSetId>>,
          IParseVisitor<RefToken<CombinationSetId>>
    {
        public void Visit([NotNull] IParseDirector dir, [NotNull] CombinationSetBeginToken token)
        {
            var newCaseSetNode = new CombinationSetNode(new CombinationSetId(token.Owner.Name), token.CodeLocation, token.OnlyPairwise);

            dir.AddId(token.Owner, newCaseSetNode);
            dir.PushScope(newCaseSetNode);
        }

        public void Visit([NotNull] IParseDirector dir, [NotNull] EndToken<CombinationSetId> token)
        {
            dir.PopScope();
        }

        public void Visit([NotNull] IParseDirector dir, [NotNull] RefToken<CombinationSetId> token)
        {
            CodeLocation codeLocation = token.CodeLocation;

            IDefNode referredSetNode = dir.GetNodeForId<IProdNode>(token.Owner, codeLocation);

            var newNode = new RefNode<IProdNode>((IProdNode)referredSetNode, codeLocation);

            dir.AddChildToScope(newNode);
        }
    }
}