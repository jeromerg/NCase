using System;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.Print;
using NDsl.Back.Api.RecPlay;

namespace NCaseFramework.Back.Imp.InterfaceRecPlay
{
    public class PrintCaseVisitors : IPrintCaseVisitor<IInterfaceRecPlayNode>
    {
        public void Visit([NotNull] IPrintCaseDirector director,
                          [NotNull] IInterfaceRecPlayNode node,
                          [NotNull] IPrintCasePayload payload)
        {
            if (payload == null) throw new ArgumentNullException("payload");

            payload.PrintFact(node.CodeLocation, node.PrintAssignment());
        }
    }
}