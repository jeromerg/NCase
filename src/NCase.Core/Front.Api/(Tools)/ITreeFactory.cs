using JetBrains.Annotations;
using NCase.Back.Api.Tree;
using NCase.Front.Api;
using NCase.Front.Ui;
using NDsl.All;
using NDsl.Back.Api.Core;

namespace NCase.Front.Imp
{
    public interface ITreeFactory : ITool<IBuilderApi>
    {
        ITree Create([NotNull] string defName, [NotNull] IBook book);
    }
}