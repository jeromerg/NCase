using NDsl.Api.Dev.Core.Nod;
using NVisitor.Api.PairBatch;

namespace NCase.Api.Dev.Core.Parse
{
    public interface IAddChildDirector : IPairDirector<INode, INode, IAddChildDirector>
    {
    }
}