using JetBrains.Annotations;
using NCaseFramework.Back.Api.CombinationSet;
using NCaseFramework.Back.Api.Print;
using NDsl.Back.Api.Util;
using NUtil.Linq;

namespace NCaseFramework.Back.Imp.CombinationSet
{
    public class PrintDefinitionVisitors
        : IPrintDefinitionVisitor<ICombinationSetNode>,
          IPrintDefinitionVisitor<IProdNode>,
          IPrintDefinitionVisitor<IPairwiseProdNode>,
          IPrintDefinitionVisitor<IUnionNode>,
          IPrintDefinitionVisitor<IBranchNode>
    {
        public void Visit([NotNull] IPrintDefinitionDirector dir,
                          [NotNull] ICombinationSetNode node,
                          [NotNull] IPrintDefinitionPayload p)
        {
            p.PrintLine(node.CodeLocation, "Combination Set '{0}'", node.Id.Name);
            p.Indent();
            dir.Visit(node.Product, p);
            p.Dedent();
        }

        public void Visit([NotNull] IPrintDefinitionDirector dir,
                          [NotNull] IPairwiseProdNode node,
                          [NotNull] IPrintDefinitionPayload p)
        {
            // add empty line after visiting each union node
            node.Unions.ForEach(unionNode => dir.Visit(unionNode, p), () => p.PrintLine(CodeLocation.Unknown, ""));
        }

        public void Visit([NotNull] IPrintDefinitionDirector dir,
                          [NotNull] IProdNode node,
                          [NotNull] IPrintDefinitionPayload p)
        {
            // add empty line after visiting each union node
            node.Unions.ForEach(unionNode => dir.Visit(unionNode, p), () => p.PrintLine(CodeLocation.Unknown, ""));
        }

        public void Visit([NotNull] IPrintDefinitionDirector dir, [NotNull] IUnionNode node, [NotNull] IPrintDefinitionPayload p)
        {
            node.Branches.ForEach(branchNode => dir.Visit(branchNode, p));
        }

        public void Visit([NotNull] IPrintDefinitionDirector dir, [NotNull] IBranchNode node, [NotNull] IPrintDefinitionPayload p)
        {
            dir.Visit(node.Declaration, p);
            p.Indent();
            dir.Visit(node.Product, p);
            p.Dedent();
        }
    }
}