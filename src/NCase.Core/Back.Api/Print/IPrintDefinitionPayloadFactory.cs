using JetBrains.Annotations;

namespace NCaseFramework.Back.Api.Print
{
    public interface IPrintDefinitionPayloadFactory
    {
        [NotNull]
        IPrintDefinitionPayload Create(bool isFileInfo, bool isRecursive);
    }
}