using System.Runtime.CompilerServices;

namespace NCaseFramework.Test.Util
{
    public class LineUtil
    {
        public static string GetLine(int offset, [CallerLineNumber] int callerLineNumber = -1)
        {
            return "line " + (offset + callerLineNumber);
        }
    }
}