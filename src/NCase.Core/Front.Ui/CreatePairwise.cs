using NDsl.Front.Ui;

namespace NCase.Front.Ui
{
    public class CreatePairwise : IOp<IBuilder, IPairwise>
    {
        private readonly string mName;

        public CreatePairwise(string name)
        {
            mName = name;
        }

        public string Name
        {
            get { return mName; }
        }
    }
}