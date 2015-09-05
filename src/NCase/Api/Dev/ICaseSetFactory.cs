using NDsl.Api.Core;

namespace NCase.Api.Dev
{
    public interface ICaseSetFactory
    {
    }

    public interface ICaseSetFactory<out T>  : ICaseSetFactory
        where T : ICaseSet
    {
        T Create(ITokenWriter tokenWriter, string name);
    }
}
