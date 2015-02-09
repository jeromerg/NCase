using System.Text;
using NDsl.Api.Core;
using NVisitor.Api.Batch;

namespace NDsl.Impl.Core
{
    public class DumpDir : Director<INode, IDumpDir>, IDumpDir
    {
        private const int INDENTATION_SPACES = 4;
        private int mCurrentIndentation;

        private readonly StringBuilder mStringBuilder = new StringBuilder();

        public DumpDir(IVisitMapper<INode, IDumpDir> visitMapper) : base(visitMapper)
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