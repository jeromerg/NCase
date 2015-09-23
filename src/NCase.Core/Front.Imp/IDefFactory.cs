using NCase.Front.Api;
using NDsl.Back.Api.Core;

namespace NCase.Front.Imp
{
    public interface IDefFactory
    {
    }

    public interface IDefFactory<out T> : IDefFactory
        where T : IDef
    {
        T Create(ITokenReaderWriter tokenReaderWriter, string name);
    }
}