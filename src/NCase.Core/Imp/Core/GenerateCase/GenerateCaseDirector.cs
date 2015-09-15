using System.Collections.Generic;
using NCase.Api.Dev.Core.GenerateCase;
using NDsl.Api.Dev.Core.Nod;
using NVisitor.Api.Func;

namespace NCase.Imp.Core.GenerateCase
{
    public class GenerateCaseDirector : FuncDirector<INode, IGenerateCaseDirector, IEnumerable<List<INode>>>, IGenerateCaseDirector
    {
        public GenerateCaseDirector(IEnumerable<IFuncVisitorClass<INode, IGenerateCaseDirector, IEnumerable<List<INode>>>> visitors)
            : base(visitors)
        {
        }
    }
}