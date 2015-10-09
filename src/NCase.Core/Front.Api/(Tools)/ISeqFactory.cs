using JetBrains.Annotations;
using NCase.Back.Api.Seq;
using NCase.Front.Api;
using NCase.Front.Ui;
using NDsl.All;
using NDsl.Back.Api.Core;

namespace NCase.Front.Imp
{
    public interface ISeqFactory: ITool<IBuilderModel>
    {
        ISeq Create([NotNull] string defName, [NotNull] IBook book);
    }
}