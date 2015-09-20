using NDsl.Api.Dev.Core;

namespace NCase.Api.Dev.Core
{
    public interface IDefFactory
    {
    }

    public interface IDefFactory<out T>  : IDefFactory
        where T : IDef
    {
        T Create(ITokenReaderWriter tokenReaderWriter, string name);
    }
}
