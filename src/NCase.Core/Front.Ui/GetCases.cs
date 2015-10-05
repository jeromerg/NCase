using NDsl.Front.Ui;

namespace NCase.Front.Ui
{
    public class GetCases : IOp<ISetDef, ICaseEnumerable>
    {
        public static readonly GetCases Instance = new GetCases();

        private GetCases()
        {
        }
    }
}