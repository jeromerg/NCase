using System.Collections.Generic;
using NCase.Back.Api.Core.Parse;
using NDsl.Api.Dev.Core.Nod;
using NDsl.Api.Dev.RecPlay;

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