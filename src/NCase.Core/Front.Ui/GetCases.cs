using NDsl.Front.Ui;

namespace NCase.Front.Ui
{
    public class GetCases : IOp<ISetDef, ICaseEnumerable>
    {
        public static readonly GetCases Default = new GetCases(true);

        private GetCases(bool isRecursive)
        {
            IsRecursive = isRecursive;
        }

        public bool IsRecursive { get; private set; }
    }
}