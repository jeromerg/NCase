using System.Collections.Generic;
using NCase.Back.Api.Parse;
using NDsl.Api.Core;
using NDsl.Api.RecPlay;

namespace NCase.Back.Imp.InterfaceRecPlay
{
    public class GenerateCaseVisitors
        : IGenerateCaseVisitor<IInterfaceRecPlayNode>
    {
        public IEnumerable<List<INode>> Visit(IGenerateDirector director, IInterfaceRecPlayNode node)
        {
            yield return new List<INode> {node};
        }
    }
}