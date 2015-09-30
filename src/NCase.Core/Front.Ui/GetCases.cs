using System.Collections.Generic;
using NDsl.Front.Ui;

namespace NCase.Front.Ui
{
    public class GetCases : IOp<ISetDef, IEnumerable<ICase>>
    {
        public static readonly GetCases Instance = new GetCases();

        private GetCases()
        {
        }
    }
}