using NDsl.Front.Ui;

namespace NCase.Front.Ui
{
    public class CreateProd : IOp<IBuilder, IProd>
    {
        private readonly string mName;

        public CreateProd(string name)
        {
            mName = name;
        }

        public string Name
        {
            get { return mName; }
        }
    }
}