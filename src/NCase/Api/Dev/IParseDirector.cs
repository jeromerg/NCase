using System.Collections.Generic;
using NCase.Imp.Core;
using NCase.Imp.Tree;
using NDsl.Api.Core.Tok;
using NVisitor.Api.Batch;

namespace NCase.Api.Dev
{
    public interface IParseDirector : IDirector<IToken, IParseDirector>
    {
        Dictionary<ICaseSet, ICaseSetNode<ICaseSet>> AllCaseSets { get; }
        
        ICaseSetNode CurrentCaseSet { get; set; }
    }
}