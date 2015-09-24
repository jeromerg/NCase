using System.Collections.Generic;
using NCase.Back.Api.Parse;
using NDsl.Api.Core;
using NVisitor.Api.Func;

namespace NCase.Back.Imp.Parse
{
    public class GenerateDirector : FuncDirector<INode, IGenerateDirector, IEnumerable<List<INode>>>, IGenerateDirector
    {
        public GenerateDirector(IEnumerable<IFuncVisitorClass<INode, IGenerateDirector, IEnumerable<List<INode>>>> visitors)
            : base(visitors)
        {
        }
    }
}