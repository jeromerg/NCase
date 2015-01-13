using System.Collections.Generic;
using System.Text;
using NTestCase.Util.Visit;

namespace NTestCase.Api.Dev.Dir
{
    public class DumpDirector : Director<DumpDirector, INode<ITarget>>
    {
        private const int INDENTATION_SPACES = 4;
        private int mCurrentIndentation;

        private readonly StringBuilder mStringBuilder = new StringBuilder();

        public DumpDirector(IEnumerable<IVisitor<DumpDirector, INode<ITarget>>> visitors)
            : base(visitors)
        {
        }

        public void Indent()
        {
            mCurrentIndentation++;
        }

        public void Dedent()
        {
            mCurrentIndentation--;
        }

        public void AddText(string format, params object[] formatArgs)
        {
            mStringBuilder.Append(new string(' ', INDENTATION_SPACES * mCurrentIndentation));
            mStringBuilder.AppendFormat(format, formatArgs);
            mStringBuilder.AppendLine();
        }


        public override string ToString()
        {
            return mStringBuilder.ToString();
        }
    }
}