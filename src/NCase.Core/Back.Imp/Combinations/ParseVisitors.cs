using JetBrains.Annotations;
using NCaseFramework.Back.Api.Combinations;
using NCaseFramework.Back.Api.Parse;
using NDsl.Back.Api.Def;
using NDsl.Back.Api.Ref;
using NDsl.Back.Api.Util;

namespace NCaseFramework.Back.Imp.Combinations
{
    public class ParseVisitors
        : IParseVisitor<BeginToken<CombinationsId>>,
          IParseVisitor<EndToken<CombinationsId>>,
          IParseVisitor<RefToken<CombinationsId>>
    {
        public void Visit([NotNull] IParseDirector dir, [NotNull] BeginToken<CombinationsId> token)
        {
            var newCaseSetNode = new CombinationsNode(token.CodeLocation, token.Owner, null);

            dir.AddId(token.Owner, newCaseSetNode);
            dir.PushScope(newCaseSetNode);
        }

        public void Visit([NotNull] IParseDirector dir, [NotNull] EndToken<CombinationsId> token)
        {
            dir.PopScope();
        }

        public void Visit([NotNull] IParseDirector dir, [NotNull] RefToken<CombinationsId> token)
        {
            CodeLocation codeLocation = token.CodeLocation;

            IDefNode referredSetNode = dir.GetNodeForId<ICombinationsNode>(token.Owner, codeLocation);

            var newNode = new RefNode<ICombinationsNode>((ICombinationsNode)referredSetNode, codeLocation);

            dir.AddChildToScope(newNode);
        }
    }
}