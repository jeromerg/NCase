using NDsl.Back.Api.Common;

namespace NCaseFramework.Front.Api.Fact
{
    public interface IFactModel
    {
        // TODO JRG: ADD STREAM TO CHANGE MODE (MAYBE ONLY A (NEW) BASE INTERFACE CONTAINING ONLY MODE
        INode FactNode { get; }
    }
}