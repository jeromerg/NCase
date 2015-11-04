using JetBrains.Annotations;
using NDsl.Back.Api.Util;

namespace NCaseFramework.Back.Api.Print
{
    public interface IPrintCasePayload
    {
        [StringFormatMethod("args")]
        void PrintFact(CodeLocation codeLocation, string format, params object[] args);

        string GetString();
    }
}