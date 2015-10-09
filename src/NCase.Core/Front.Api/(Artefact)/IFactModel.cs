using NDsl.Back.Api.Core;
using NDsl.Front.Ui;

namespace NCase.Front.Api
{
    public interface IFactModel : IArtefactModel
    {
        INode FactNode { get; }
    }
}