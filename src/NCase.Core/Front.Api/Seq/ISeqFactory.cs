using JetBrains.Annotations;
using NCase.Front.Api.Builder;
using NCase.Front.Ui;
using NDsl.Back.Api.Book;
using NDsl.Back.Api.Util;

namespace NCase.Front.Api.Seq
{
    public interface ISeqFactory: IService<IBuilderModel>
    {
        ISeq Create([NotNull] string defName, [NotNull] IBook book);
    }
}