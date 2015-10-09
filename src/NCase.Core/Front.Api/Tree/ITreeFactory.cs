using JetBrains.Annotations;
using NCase.Front.Api.Builder;
using NCase.Front.Ui;
using NDsl.Back.Api.Book;
using NDsl.Back.Api.Util;

namespace NCase.Front.Api.Tree
{
    public interface ITreeFactory : IService<IBuilderModel>
    {
        ITree Create([NotNull] string defName, [NotNull] ITokenStream tokenStream);
    }
}