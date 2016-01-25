using JetBrains.Annotations;

namespace NDsl.Back.Api.Util
{
    public interface IFileCache
    {
        string GetLine(string fileName, int lineIndex);
        string[] GetFileLines([NotNull] string fileName);
    }
}