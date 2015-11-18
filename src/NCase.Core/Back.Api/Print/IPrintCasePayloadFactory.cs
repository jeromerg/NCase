using JetBrains.Annotations;

namespace NCaseFramework.Back.Api.Print
{
    public interface IPrintCasePayloadFactory
    {
        [NotNull]
        IPrintCasePayload Create();
    }
}