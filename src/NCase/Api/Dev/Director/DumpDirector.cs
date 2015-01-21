using System.Collections.Generic;
using System.Text;
using NVisitor.Api;
using NVisitor.Api.Marker;

namespace NCase.Api.Dev.Director
{
    public class DumpDirector : Director<INode, DumpDirector>
    {
        private const int INDENTATION_SPACES = 4;
        private int mCurrentIndentation;

        private readonly StringBuilder mStringBuilder = new StringBuilder();

        public DumpDirector(IEnumerable<IVisitor<INode, DumpDirector>> visitors)
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