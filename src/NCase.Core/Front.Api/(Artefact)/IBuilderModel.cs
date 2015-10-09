using JetBrains.Annotations;
using NDsl.Back.Api.Core;
using NDsl.Front.Ui;

namespace NCase.Front.Api
{
    public interface IBuilderModel : IArtefactModel
    {
        [NotNull] IBook Book { get; }
    }
}