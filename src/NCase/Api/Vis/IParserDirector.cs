using System.Collections.Generic;
using NCase.Api.Nod;
using NDsl.Api.Core;
using NVisitor.Api.Batch;

namespace NCase.Api.Vis
{
    public interface IParserDirector : IDirector<IToken, IParserDirector>
    {
        Dictionary<ICaseSet, ICaseSetNode> AllCaseSets { get; }
        ICaseSetNode CurrentCaseSetNode { get; set; }
    }
}