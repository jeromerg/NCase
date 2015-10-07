using NDsl.Front.Ui;

namespace NCase.Front.Ui
{
    public class CreateTree : IOp<IBuilder, ITree>
    {
        private readonly string mName;

        public CreateTree(string name)
        {
            mName = name;
        }

        public string Name
        {
            get { return mName; }
        }
    }
}