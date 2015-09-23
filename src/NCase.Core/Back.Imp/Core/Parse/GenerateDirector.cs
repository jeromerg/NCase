using System.Collections.Generic;
using NCase.Back.Api.Core.Parse;
using NDsl.Api.Dev.Core.Nod;
using NVisitor.Api.Func;

namespace NCase.Back.Imp.Core.Parse
{
    public class GenerateDirector : FuncDirector<INode, IGenerateDirector, IEnumerable<List<INode>>>, IGenerateDirector
    {
        public GenerateDirector(IEnumerable<IFuncVisitorClass<INode, IGenerateDirector, IEnumerable<List<INode>>>> visitors)
            : base(visitors)
        {
        }
    }
}