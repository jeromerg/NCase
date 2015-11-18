using JetBrains.Annotations;

namespace NDsl.Back.Api.Record
{
    public interface ITokenStreamFactory
    {
        [NotNull]
        ITokenStream Create();
    }
}