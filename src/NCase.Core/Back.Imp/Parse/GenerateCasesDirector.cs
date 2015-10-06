using System.Collections.Generic;
using NCase.Back.Api.Parse;
using NDsl.Back.Api.Core;
using NVisitor.Api.FuncPayload;

namespace NCase.Back.Imp.Parse
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