using System;
using System.Collections.Generic;
using NCase.Api.Dev.Core.GenerateCase;
using NDsl.Api.Dev.Core.Nod;
using NDsl.Api.Dev.RecPlay;
using NVisitor.Api.Lazy;

namespace NCase.Imp.InterfaceRecPlay
{
    public class GenerateCaseVisitors
        : IGenerateCaseVisitor<IInterfaceRecPlayNode>
    {
        public IEnumerable<List<INode>> Visit(IGenerateCaseDirector director, IInterfaceRecPlayNode node)
        {
            yield return new List<INode> { node };
        }
    }
}
