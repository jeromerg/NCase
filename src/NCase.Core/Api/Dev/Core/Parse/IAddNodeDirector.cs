using NDsl.Api.Core.Nod;
using NVisitor.Api.PairBatch;

namespace NCase.Api.Dev.Core.Parse
{
    public interface IAddNodeDirector : IPairDirector<INode /*existing set*/, INode /*node to add*/, IAddNodeDirector>
    {
    }
}