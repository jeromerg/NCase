using System;
using NCaseFramework.Back.Api.Pairwise;
using NCaseFramework.Back.Api.Print;

namespace NCaseFramework.Back.Imp.Pairwise
{
    public class PrintDefinitionVisitors
        : IPrintDefinitionVisitor<IPairwiseNode>,
          IPrintDefinitionVisitor<IPairwiseDimNode>
    {
        public void Visit(IPrintDefinitionDirector director, IPairwiseDimNode node)
        {
            throw new NotImplementedException(); // TODO JRG
        }

        public void Visit(IPrintDefinitionDirector director, IPairwiseNode node)
        {
            throw new NotImplementedException(); // TODO JRG
        }
    }
}