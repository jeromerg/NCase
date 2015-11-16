using JetBrains.Annotations;
using NDsl.Back.Api.Builder;
using NDsl.Back.Api.TokenStream;
using NDsl.Back.Api.Util;
using NDsl.Front.Ui;

namespace NDsl.Front.Api
{
    public interface IDefFactory<out TDef> : IService<IBuilderModel>
        where TDef : DefBase
    {
        TDef Create([NotNull] string defName, [NotNull] ITokenStream tokenStream);
    }
}