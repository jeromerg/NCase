using System;
using NCase.Back.Api.Pairwise;
using NCase.Back.Api.Print;

namespace NCase.Back.Imp.Pairwise
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