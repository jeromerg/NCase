using NCase.Front.Api;
using NDsl.Api.Core;

namespace NCase.Front.Imp
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