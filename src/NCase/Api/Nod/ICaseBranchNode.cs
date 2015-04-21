using System.Collections.Generic;
using NDsl.Api.Core;

namespace NCase.Api.Nod
{
    public interface ICaseBranchNode : IExtendableNode, ICodeLocatedObject
    {
        INode CaseFact { get; }
        IEnumerable<INode> SubLevels { get; }
    }
}