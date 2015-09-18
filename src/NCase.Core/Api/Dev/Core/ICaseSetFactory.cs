using NDsl.Api.Dev.Core;

namespace NCase.Api.Dev.Core
{
    public interface ICaseSetFactory
    {
    }

    public interface ICaseSetFactory<out T>  : ICaseSetFactory
        where T : ICaseSet
    {
        T Create(ITokenReaderWriter tokenReaderWriter, string name);
    }
}
