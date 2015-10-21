using System.Collections.Generic;
using NCaseFramework.Back.Api.Parse;
using NDsl.Back.Api.Common;
using NVisitor.Api.FuncPayload;

namespace NCaseFramework.Back.Imp.Parse
{
    public class GenerateCasesDirector
        : FuncPayloadDirector<INode, IGenerateCasesDirector, GenerateOptions, IEnumerable<List<INode>>>, IGenerateCasesDirector
    {
        public GenerateCasesDirector(
            IEnumerable<IFuncPayloadVisitorClass<INode, IGenerateCasesDirector, GenerateOptions, IEnumerable<List<INode>>>>
                visitors)
            : base(visitors)
        {
        }
    }
}