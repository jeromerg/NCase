using System.Collections.Generic;
using NCaseFramework.Back.Api.Parse;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.RecPlay;

namespace NCaseFramework.Back.Imp.InterfaceRecPlay
{
    public class GenerateCaseVisitors
        : IGenerateCaseVisitor<IInterfaceRecPlayNode>
    {
        public IEnumerable<List<INode>> Visit(IGenerateCasesDirector dir, IInterfaceRecPlayNode node, GenerateOptions options)
        {
            yield return new List<INode> {node};
        }
    }
}