using NDsl.Front.Ui;

namespace NCase.Front.Ui
{
    public class CreateSeq : IOp<IBuilder, ISeq>
    {
        private readonly string mName;

        public CreateSeq(string name)
        {
            mName = name;
        }

        public string Name
        {
            get { return mName; }
        }
    }
}