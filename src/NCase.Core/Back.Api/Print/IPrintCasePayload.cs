using JetBrains.Annotations;
using NDsl.Back.Api.Util;

namespace NCaseFramework.Back.Api.Print
{
    public interface IPrintCasePayload
    {
        [StringFormatMethod("args")]
        void PrintFact([NotNull] CodeLocation codeLocation, [NotNull] string format, [NotNull] params object[] args);

        [NotNull] string GetString();
    }
}