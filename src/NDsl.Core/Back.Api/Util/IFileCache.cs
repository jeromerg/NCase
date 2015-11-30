namespace NDsl.Back.Api.Util
{
    public interface IFileCache
    {
        string GetLine(string fileName, int lineIndex); 
        int GetIndentation(string fileName, int lineIndex); 
    }
}