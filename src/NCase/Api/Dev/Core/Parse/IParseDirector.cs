using System.Collections.Generic;
using NCase.Api.Dev.Core.CaseSet;
using NDsl.Api.Core.Tok;
using NVisitor.Api.Batch;

namespace NCase.Api.Dev.Core.Parse
{
    public interface IParseDirector : IDirector<IToken, IParseDirector>
    {
        Dictionary<ICaseSet, ICaseSetNode<ICaseSet>> AllCaseSets { get; }
        
        ICaseSetNode CurrentCaseSet { get; set; }
    }
}