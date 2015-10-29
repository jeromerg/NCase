using System.Runtime.CompilerServices;

namespace NUtil.File
{
    public static class CallerUtil
    {
        public static string GetCallerFilePath([CallerFilePath] string callerFilePath = null)
        {
            return callerFilePath;
        }

        public static string GetCallerMemberName([CallerMemberName] string callerMemberName = null)
        {
            return callerMemberName;
        }

        public static int GetCallerLineNumber([CallerLineNumber] int callerLineNumber = -1)
        {
            return callerLineNumber;
        }
    }
}