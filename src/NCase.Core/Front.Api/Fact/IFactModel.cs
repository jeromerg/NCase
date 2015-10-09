using NDsl.Back.Api.Common;
using NDsl.Front.Api;

namespace NCase.Front.Api.Fact
{
    public interface IFactModel : IArtefactModel
    {
        INode FactNode { get; }
    }
}