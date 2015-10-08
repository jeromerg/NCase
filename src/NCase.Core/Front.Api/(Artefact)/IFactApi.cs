using NDsl.Back.Api.Core;
using NDsl.Front.Ui;

namespace NCase.Front.Api
{
    public interface IFactApi : IArtefactApi<IFactApi>
    {
        INode FactNode { get; }
    }
}