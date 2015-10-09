using JetBrains.Annotations;
using NCase.Front.Api.Builder;
using NCase.Front.Ui;
using NDsl.Back.Api.Book;
using NDsl.Back.Api.Util;

namespace NCase.Front.Api.Prod
{
    public interface IProdFactory : IService<IBuilderModel>
    {
        IProd Create([NotNull] string defName, [NotNull] IBook book);
    }
}