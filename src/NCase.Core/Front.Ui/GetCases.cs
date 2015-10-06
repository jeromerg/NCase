using NDsl.Front.Ui;

namespace NCase.Front.Ui
{
    public class GetCases : IOp<ISetDef, ICaseEnumerable>
    {
        public static readonly GetCases Default = new GetCases();

        private GetCases()
        {
        }
    }
}