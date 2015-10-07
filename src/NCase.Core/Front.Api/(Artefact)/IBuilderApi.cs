using NDsl.Back.Api.Core;
using NDsl.Front.Ui;

namespace NCase.Front.Api
{
    public interface IBuilderApi : IArtefactApi
    {
        IBook Book { get; }
    }
}