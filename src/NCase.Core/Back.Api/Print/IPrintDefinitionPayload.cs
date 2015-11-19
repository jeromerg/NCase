using JetBrains.Annotations;
using NDsl.Back.Api.Util;

namespace NCaseFramework.Back.Api.Print
{
    public interface IPrintDefinitionPayload
    {
        bool IsRecursive { get; }

        void Indent();
        void Dedent();

        [StringFormatMethod("args")]
        void PrintLine([NotNull] CodeLocation codeLocation, [NotNull] string format, [NotNull] params object[] args);

        [NotNull]
        string GetString();
    }
}