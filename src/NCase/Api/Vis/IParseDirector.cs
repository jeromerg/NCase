using System.Collections.Generic;
using NCase.Api.Nod;
using NCase.Imp.Nod;
using NDsl.Api.Core;
using NDsl.Api.Core.Nod;
using NDsl.Api.Core.Tok;
using NVisitor.Api.Batch;

namespace NCase.Api.Vis
{
    public interface IParseDirector : IDirector<IToken, IParseDirector>
    {
        Dictionary<ICaseSet, ICaseSetNode> AllCaseSets { get; }
        // TODO: GENERALIZE TO ANY CASE SET TYPE
        IExtendableNode CurrentCaseSetNode { get; set; }
    }
}