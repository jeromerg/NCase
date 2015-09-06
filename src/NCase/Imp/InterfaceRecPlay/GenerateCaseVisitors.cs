using System;
using System.Collections.Generic;
using NCase.Api.Dev;
using NDsl.Api.Core.Nod;
using NDsl.Api.RecPlay;
using NVisitor.Api.Lazy;

namespace NCase.Imp.InterfaceRecPlay
{
    public class GenerateCaseVisitors
        : ILazyVisitor<INode, IGenerateCaseDirector, IInterfaceRecPlayNode>
    {
        public IEnumerable<Pause> Visit(IGenerateCaseDirector director, IInterfaceRecPlayNode node)
        {
            IDisposable popHandle = director.Push(node);
            yield return Pause.Now;
            popHandle.Dispose();
        }
    }
}
