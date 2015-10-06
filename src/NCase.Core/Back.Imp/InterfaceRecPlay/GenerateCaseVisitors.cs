using System.Collections.Generic;
using NCase.Back.Api.Parse;
using NDsl.Back.Api.Core;
using NDsl.Back.Api.RecPlay;

namespace NCase.Back.Imp.InterfaceRecPlay
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