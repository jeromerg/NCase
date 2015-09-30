using NDsl.Back.Api.Core;
using NDsl.Front.Api;

namespace NDsl.Front.Imp
{
    public interface IDefFactory
    {
    }

    public interface IDefFactory<out TDef> : IDefFactory
        where TDef : IDef<TDef>
    {
        TDef Create(ITokenReaderWriter tokenReaderWriter, string name);
    }
}