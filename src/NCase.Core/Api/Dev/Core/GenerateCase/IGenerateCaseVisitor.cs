using NDsl.Api.Dev.Core.Nod;
using NVisitor.Api.Lazy;

namespace NCase.Api.Dev.Core.GenerateCase
{
    public interface IGenerateCaseVisitor<TNod> : ILazyVisitor<INode, IGenerateCaseDirector, TNod>
        where TNod : INode
    {
    }
}