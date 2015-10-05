using NDsl.Front.Ui;

namespace NCase.Front.Ui
{
    public class Replay : IOp<ICaseEnumerable, ICaseEnumerable>
    {
        public static readonly Replay Default = new Replay();
    }
}