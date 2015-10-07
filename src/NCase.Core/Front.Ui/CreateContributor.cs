using NDsl.Front.Ui;

namespace NCase.Front.Ui
{
    public class CreateContributor<TContrib>: IOp<IBuilder>
    {
        private readonly string mName;

        public CreateContributor(string name)
        {
            mName = name;
        }

        public string Name
        {
            get { return mName; }
        }
    }
}