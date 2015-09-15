using NDsl.Api.Dev.Core;

namespace NCase.Api.Dev.Core.CaseSet
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
