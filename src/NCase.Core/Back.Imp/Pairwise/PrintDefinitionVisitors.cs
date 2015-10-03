using System;
using NCase.Back.Api.Pairwise;
using NCase.Back.Api.Print;
using NDsl.Back.Api.Ref;

namespace NCase.Back.Imp.Pairwise
{
    public class PrintDefinitionVisitors
        : IPrintDefinitionVisitor<IPairwiseNode>,
          IPrintDefinitionVisitor<IPairwiseDimNode>,
          IPrintDefinitionVisitor<IRefNode<IPairwiseNode>>
    {
        public void Visit(IPrintDefinitionDirector director, IPairwiseDimNode node)
        {
            throw new NotImplementedException(); // TODO JRG
        }

        public void Visit(IPrintDefinitionDirector director, IPairwiseNode node)
        {
            throw new NotImplementedException(); // TODO JRG
        }

        public void Visit(IPrintDefinitionDirector dir, IRefNode<IPairwiseNode> node)
        {
            if (dir.RecurseIntoReferences)
                dir.Visit(node.Reference);
            else
                dir.Print(node.CodeLocation, "Ref to PAIRWISE {0}", node.Reference.Id.Name);
        }
    }
}