using System.Text;
using NDsl.Api.Core.Nod;
using NDsl.Api.Core.Vis;
using NVisitor.Api.Batch;

namespace NDsl.Imp.Core
{
    public class DumpDirector : Director<INode, IDumpDirector>, IDumpDirector
    {
        private const int INDENTATION_SPACES = 4;
        private int mCurrentIndentation;

        private readonly StringBuilder mStringBuilder = new StringBuilder();

        public DumpDirector(IVisitMapper<INode, IDumpDirector> visitMapper) : base(visitMapper)
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