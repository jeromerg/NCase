using JetBrains.Annotations;
using NCaseFramework.Back.Api.CombinationSet;
using NCaseFramework.Back.Api.Parse;
using NDsl.Back.Api.Def;
using NDsl.Back.Api.Ref;
using NDsl.Back.Api.Util;

namespace NCaseFramework.Back.Imp.CombinationSet
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

            IDefNode referredSetNode = dir.GetNodeForId<ICombinationSetNode>(token.Owner, codeLocation);

            var newNode = new RefNode<ICombinationSetNode>((ICombinationSetNode)referredSetNode, codeLocation);

            dir.AddChildToScope(newNode);
        }
    }
}