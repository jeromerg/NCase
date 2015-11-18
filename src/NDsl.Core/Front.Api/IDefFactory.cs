using JetBrains.Annotations;
using NDsl.Back.Api.Builder;
using NDsl.Back.Api.Record;
using NDsl.Back.Api.Util;
using NDsl.Front.Ui;

namespace NDsl.Front.Api
{
    public interface IDefFactory<out TDef> : IService<ICaseBuilderModel>
        where TDef : DefBase
    {
        [NotNull]
        TDef Create([NotNull] string defName, [NotNull] ITokenStream tokenStream);
    }
}