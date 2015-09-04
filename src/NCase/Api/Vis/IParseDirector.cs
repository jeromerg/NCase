using System.Collections.Generic;
using NCase.Imp.Nod;
using NDsl.Api.Core.Tok;
using NVisitor.Api.Batch;

namespace NCase.Api.Vis
{
    public interface IParseDirector : IDirector<IToken, IParseDirector>
    {
        Dictionary<ICaseSet, ICaseTreeNode> AllCaseSets { get; }
        // TODO: GENERALIZE TO ANY CASE SET TYPE
        ICaseTreeNode CurrentSetNode { get; set; }
    }
}