namespace NDsl.Back.Api.Util
{
    public interface IFileAnalyzer
    {
        bool IsEmptyLine(string fileName, int lineIndex);
        int GetIndentation(string fileName, int lineIndex);
    }
}