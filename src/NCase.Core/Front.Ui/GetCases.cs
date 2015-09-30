using System.Collections.Generic;
using NDsl.Front.Api;

namespace NCase.Front.Api
{
    public class GetCases : IOp<ISetDef, IEnumerable<ICase>>
    {
        public static readonly GetCases Instance = new GetCases();

        private GetCases()
        {
        }
    }
}