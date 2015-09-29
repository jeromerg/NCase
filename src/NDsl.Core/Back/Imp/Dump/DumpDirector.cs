using System.Text;
using NDsl.Back.Api.Core;
using NDsl.Back.Api.Dump;
using NVisitor.Api.Action;

namespace NDsl.Back.Imp.Dump
{
    public class DumpDirector : ActionDirector<INode, IDumpDirector>, IDumpDirector
    {
        private const int INDENTATION_SPACES = 4;

        private readonly StringBuilder mStringBuilder = new StringBuilder();
        private int mCurrentIndentation;

        public DumpDirector(IActionVisitMapper<INode, IDumpDirector> visitMapper)
            : base(visitMapper)
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
            mStringBuilder.Append(new string(' ', INDENTATION_SPACES*mCurrentIndentation));
            mStringBuilder.AppendFormat(format, formatArgs);
            mStringBuilder.AppendLine();
        }


        public override string ToString()
        {
            return mStringBuilder.ToString();
        }
    }
}