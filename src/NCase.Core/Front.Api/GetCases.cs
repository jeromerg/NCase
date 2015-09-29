using System.Collections.Generic;

namespace NCase.Front.Api
{
    public class GetCases : IOp<IDef, IEnumerable<ICase>>
    {
        public static readonly GetCases Instance = new GetCases();

        private GetCases()
        {
        }
    }
}