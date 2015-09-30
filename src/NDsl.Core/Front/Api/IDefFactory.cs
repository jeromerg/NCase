using NDsl.Back.Api.Core;
using NDsl.Front.Ui;

namespace NDsl.Front.Api
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